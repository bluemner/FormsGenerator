using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FormsGeneratorWebApplication.Startup))]
namespace FormsGeneratorWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
