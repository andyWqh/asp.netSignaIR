using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignaIRChatRoon.Models
{
    /// <summary>
    /// 用户信息类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户连接集合
        /// </summary>
        public List<Connection> Connections { get; set; }

        /// <summary>
        /// 用户进入的房间集合，一个用户可以加入多个聊天室
        /// 比如一个用户可以加入多个微信群或者qq群
        /// </summary>
        public List<ChatRoom> Rooms { get; set; }
        
        /// <summary>
        /// 无惨构造函数初始化用户房间集合和连接集合
        /// </summary>
        public User()
        {
            Connections = new List<Connection>();
            Rooms = new List<ChatRoom>();
        }
    
    }
}