using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignaIRChatRoon.Startup))]
namespace SignaIRChatRoon
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
