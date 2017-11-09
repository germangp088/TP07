using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TP07.Startup))]
namespace TP07
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
