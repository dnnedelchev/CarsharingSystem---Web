using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarsharingSystem.Web.Startup))]
namespace CarsharingSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
