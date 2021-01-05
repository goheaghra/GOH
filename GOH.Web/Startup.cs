using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GOH.Web.Startup))]

namespace GOH.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}