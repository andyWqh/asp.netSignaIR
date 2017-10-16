using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KnockoutJSQuickStart.Startup))]
namespace KnockoutJSQuickStart
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
