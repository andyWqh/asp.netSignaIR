using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignaIRSendImages.Startup))]
namespace SignaIRSendImages
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //启动时连接hub集线器
            app.MapSignalR();
        }
    }
}
