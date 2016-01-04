using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WforViolation.Startup))]
namespace WforViolation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
