using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SPAWithKnockOutJs.Startup))]
namespace SPAWithKnockOutJs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
