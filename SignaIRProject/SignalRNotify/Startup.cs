using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalRNotify.Startup))]
namespace SignalRNotify
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
