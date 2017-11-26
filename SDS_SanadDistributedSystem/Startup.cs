using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SDS_SanadDistributedSystem.Startup))]
namespace SDS_SanadDistributedSystem
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
