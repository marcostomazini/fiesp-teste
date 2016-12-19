using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FiespTeste.Startup))]
namespace FiespTeste
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
