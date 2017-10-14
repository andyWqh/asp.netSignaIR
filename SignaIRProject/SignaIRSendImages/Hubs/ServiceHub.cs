using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignaIRSendImages.Models;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SignaIRSendImages.Hubs
{
    public class ServiceHub:Hub
    {
        /// <summary>
        /// 供客户端调用的服务器端代码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="message"></param>
        public void Send(string name, string message)
        {
            // 调用所有客户端的sendMessage方法
            Clients.All.sendMessage(name, message);
        }

        // 发送图片
        public void SendImage(string name, IEnumerable<ImageData> images)
        {
            foreach (var item in images ?? Enumerable.Empty<ImageData>())
            {
                if (String.IsNullOrEmpty(item.Image))
                {
                    continue;
                }
                // 调用客户端receiveImage方法将图片进行显示
                Clients.All.receiveImage(name, item.Image);
            }
        }

        /// <summary>
        /// 客户端连接的时候调用
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            Trace.WriteLine("客户端连接成功");
            return base.OnConnected();
        }
    }
}