using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignaIRChatRoon.Models
{
    /// <summary>
    /// 聊天室信息类
    /// </summary>
    public class ChatRoom
    {
        /// <summary>
        /// 聊天室名称
        /// </summary>
        public string RoomName { get; set; }

        /// <summary>
        /// 聊天室含有用户 ，一个聊天室可含多个用户
        /// 类似一个微信群可含多个用户
        /// </summary>
        public List<User> Users { get; set; }

        public ChatRoom()
        {
            Users = new List<User>();
        }
    }
}