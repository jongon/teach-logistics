using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeachLogisticsTest.Startup))]
namespace TeachLogisticsTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
