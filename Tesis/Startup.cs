using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tesis.Startup))]
namespace Tesis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
