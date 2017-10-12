using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCOtel_2.Startup))]
namespace MVCOtel_2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
