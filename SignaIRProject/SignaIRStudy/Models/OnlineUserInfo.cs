using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignaIRStudy.Models
{
    /// <summary>
    /// 在线用户信息实体
    /// </summary>
    public class OnlineUserInfo
    {
        /// <summary>
        /// 连接标识Id
        /// </summary>
        public string ConnectionId { get; set; }
        
        /// <summary>
        /// 用户标识Id
        /// </summary>
        public string UserId{get;set;}

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName{get;set;}

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime{get;set;}
    }
}