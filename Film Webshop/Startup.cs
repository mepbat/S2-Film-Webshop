using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Film_Webshop.Startup))]
namespace Film_Webshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
