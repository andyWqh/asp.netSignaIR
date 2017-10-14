using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignaIRChatRoon.Startup))]
namespace SignaIRChatRoon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            //启动时连接hub集线器
            app.MapSignalR();
        }
    }
}
