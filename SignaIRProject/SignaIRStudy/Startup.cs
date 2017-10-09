using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignaIRStudy.Startup))]
namespace SignaIRStudy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
