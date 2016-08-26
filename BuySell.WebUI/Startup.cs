using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BuySell.WebUI.Startup))]
namespace BuySell.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
