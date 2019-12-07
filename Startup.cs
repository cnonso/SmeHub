using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SME_Hub.Startup))]
namespace SME_Hub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
