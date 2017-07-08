using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocLive2.Startup))]
namespace DocLive2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
