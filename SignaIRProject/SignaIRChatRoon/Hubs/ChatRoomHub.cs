using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;
using SignaIRChatRoon.Models;
using SignaIRChatRoon.Serialization;

namespace SignaIRChatRoon.Hubs
{
    [HubName("chatRoomHub")]
    public class GroupsHub : Hub
    {
        public static ChatContext DbContext = new ChatContext();

        #region IHub Members
        
        /// <summary>
        /// 重写Hub连接事件   当用户连接Hub成功后触发该事件执行
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            //根据当前连接ConnectionId 查询是否存在该用户
            var user = DbContext.Users.FirstOrDefault(m => m.UserId == Context.ConnectionId);
            //不存在则添加到用户集合中
            if(user == null)
            {
                user = new User 
                {
                   UserId = Context.ConnectionId
                };

                DbContext.Users.Add(user);
            }

            //发送房间列表,将房间名称投影到items
            var items = DbContext.Rooms.Select(p => new { p.RoomName });
            //为客户端提供查询聊天的方法
            Clients.Client(this.Context.ConnectionId).getRoomList(JsonHelper.ToJsonString(items.ToList()));
            return base.OnConnected();
        }

        /// <summary>
        /// 重写断开Hub连接事件
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            //查询用户
            var user = DbContext.Users.FirstOrDefault(m => m.UserId == Context.ConnectionId);
            if(user != null)
            {
                //断开连接时从用户集合中删除该用户
                DbContext.Users.Remove(user);
                //在该用户加入的聊天室中移除该用户
                foreach (var item in user.Rooms)
                {
                    RemoveUserFromRoom(item.RoomName);
                }
            }
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region Public Method

        /// <summary>
        /// 为所有用户更新房间列表
        /// </summary>
        public void UpdateRoomList()
        {
            var items = DbContext.Rooms.Select(p => new { p.RoomName });
            var jsonData = JsonHelper.ToJsonString(items.ToList());
            //提供客户端调用
            Clients.All.getRoomList(jsonData);
        }

        /// <summary>
        /// 加入指定聊天室
        /// </summary>
        /// <param name="roomName"></param>
        public void JoinRoom(string roomName)
        {
            //查询聊天室
            var room = DbContext.Rooms.Find(p => p.RoomName == roomName);
            //存在则加入聊天室
            if(roomName == null)
            { 
                return;
            }
            //在聊天室查询是否存在当前用户
            var isExistUser = room.Users.FirstOrDefault(m => m.UserId == Context.ConnectionId);
            //不存在当前用户则添加到聊天室用户集合中
            if(isExistUser == null)
            {
                var user = DbContext.Users.Find(m => m.UserId == Context.ConnectionId);
                user.Rooms.Add(room);
                room.Users.Add(user);

                //将客户端的连接ID加入到组里面
                Groups.Add(Context.ConnectionId, roomName);

                //调用此链接用户的本地js（显示聊天室）
                Clients.Client(Context.ConnectionId).joinRoom(roomName);
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("请勿重复加入该聊天室");
            }
        }

        /// <summary>
        /// 创建聊天室
        /// </summary>
        public void CreateRoom(string roomName)
        {
            var room = DbContext.Rooms.Find(m => m.RoomName == roomName);
            //不存在该聊天室则新建聊天室，
            if(room == null)
            {
                var chatRoom = new ChatRoom
                {
                     RoomName = roomName
                };
                //将新创建的聊天室加入到列表
                DbContext.Rooms.Add(chatRoom);
                //创建聊天室时，当前用户同时加入该聊天室
                JoinRoom(roomName);
                //更新聊天室列表
                UpdateRoomList();
            }
            else
            {
                Clients.Client(Context.ConnectionId).showMessage("聊天室重复");
            }
        }
       
        /// <summary>
        /// 从指定聊天室中删除当前用户
        /// </summary>
        /// <param name="roomName"></param>
        public  void RemoveUserFromRoom(string roomName)
        {
            //查询当前指定的聊天室是否存在
            var room = DbContext.Rooms.Find(m => m.RoomName == roomName);
            if(room == null)
            {
                Clients.Client(Context.ConnectionId).showMessage("指定聊天室不存在");
                return;
            }
            else
            {
                //查找需要删除的用户
                var user = DbContext.Users.FirstOrDefault(m => m.UserId == Context.ConnectionId);
                //从聊天室的用户集合中移除该用户
                room.Users.Remove(user);
                //如果移除该用户后，该聊天室的人数为0，则删除聊天室
                if(room.Users.Count <= 0)
                {
                    DbContext.Rooms.Remove(room);
                }
                Groups.Remove(Context.ConnectionId,roomName);
                //提示客户端
                Clients.Client(Context.ConnectionId).removeRoom("退出成功!");
            }
        }

        /// <summary>
        /// 向指定的聊天室的用户发送消息
        /// </summary>
        /// <param name="roomName">聊天室名称</param>
        /// <param name="message">消息内容</param>
        public void SendMessage(string roomName,string message)
        {
            // 调用房间内所有客户端的sendMessage方法
            // 因为在加入房间的时候，已经将客户端的ConnectionId添加到Groups对象中了，所有可以根据房间名找到房间内的所有连接Id
            // 其实我们也可以自己实现Group方法，我们只需要用List记录所有加入房间的ConnectionId
            // 然后调用Clients.Clients(connectionIdList),参数为我们记录的连接Id数组。
            Clients.Group(roomName, new string[0]).sendMessage(roomName, message + " <br/>发送时间：" + DateTime.Now);
        }
        #endregion
    }
}