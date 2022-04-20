using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BetSmartTracker.Startup))]
namespace BetSmartTracker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
