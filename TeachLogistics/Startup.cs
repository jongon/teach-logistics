using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeachLogistics.Startup))]
namespace TeachLogistics
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
