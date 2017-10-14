using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignaIRChatRoon.Models
{
    /// <summary>
    /// 用户加入聊天室的连接信息
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// 连接Id
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }


        /// <summary>
        /// 标识是否已成功连接
        /// </summary>
        public bool Connected { get; set; }
    }
}