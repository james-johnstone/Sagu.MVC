using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sagu.MVC.Startup))]
namespace Sagu.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
