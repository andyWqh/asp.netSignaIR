using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using SignaIRStudy.Models;
using Microsoft.AspNet.SignalR;

namespace SignaIRStudy.ServiceHubs
{
    public class ChatRoonService : Hub
    {
       //静态属性，存储在线用户
        public static List<OnlineUserInfo> onlineUsers = new List<OnlineUserInfo>();

        /// <summary>
        /// 登录连线
        /// </summary>
        /// <param name="userId">用户标识Id</param>
        /// <param name="userName">用户名称</param>
        public void Connect(string userId,string userName)
        {
            //获取当前连接Id
            var connectId = Context.ConnectionId;
            if(onlineUsers.Count(m => m.ConnectionId == connectId) == 0)
            {
                if(onlineUsers.Any(m => m.UserId == userId))
                {
                    var itemList = onlineUsers.Where(m => m.UserId == userId).ToList();
                    foreach (var item in itemList)
                    {
                        Clients.AllExcept(connectId).onUserDisconnected(item.ConnectionId, item.UserName);
                    }
                    onlineUsers.RemoveAll(m => m.UserId == userId);
                }
                //添加在线用户
                onlineUsers.Add(new OnlineUserInfo 
                {
                    ConnectionId = connectId,
                    UserId = userId,
                    UserName = userName,
                    LastLoginTime = DateTime.Now
                });
            }
            //所有客户端同步在线用户
            Clients.All.onConnected(connectId, userName, onlineUsers);
        }

        /// <summary>
        /// 发送私聊
        /// </summary>
        /// <param name="toUserId">接收方用户连接ID</param>
        /// <param name="message">内容</param>
        public void SendPrivateMessage(string toUserId,string message)
        {
            var fromUserId = Context.ConnectionId;
            var toUser = onlineUsers.FirstOrDefault(m =>m.ConnectionId == toUserId);
            var fromUser = onlineUsers.FirstOrDefault(m => m.ConnectionId == fromUserId);
            if(toUser != null && fromUser != null)
            {
                //send to message
                Clients.Client(toUserId).receivePrivateMessage(fromUserId, fromUser.UserName, message);
                // send to caller user
                //Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }
            else
            {
                //表示对方不在线
                Clients.Caller.absentSubscriber();
            }
        }

        /// <summary>
        /// 断线时调用
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            var user = onlineUsers.FirstOrDefault(m => m.ConnectionId == Context.ConnectionId);
            if(user == null)
            {
                return base.OnDisconnected(stopCalled);
            }
            //调用客户端用户离线通知
            Clients.All.onUserDisconnected(user.ConnectionId,user.UserName);
            //删除用户
            onlineUsers.Remove(user);
            return base.OnDisconnected(stopCalled);
        }
    }
}