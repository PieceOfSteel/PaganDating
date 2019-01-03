using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PaganDating.Startup))]
namespace PaganDating
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
