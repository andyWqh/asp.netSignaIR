using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SignaIRStudy
{
    public class ServiceHub : Hub
    {
        private static readonly char[] constant = 
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
            'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V',
            'W', 'X', 'Y', 'Z'
        };

        public void SendMessage(string message)
        {
            var name = GenerateRandomNum(4);

            //调用所有客户端的sendMessage方法
            Clients.All.sendMessage(name,message);
        }

        public override Task OnConnected()
        {
            Trace.WriteLine("客户端连接成功");
            return base.OnConnected();
        }

        /// <summary>
        /// 产生随机用户名函数
        /// </summary>
        /// <param name="length">用户名长度默认长度为4</param>
        /// <returns></returns>
        public  static string GenerateRandomNum(int lenth = 4)
        {
            var newRandom =new  StringBuilder(62);
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }
    }
}