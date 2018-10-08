using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImageWarping.Startup))]
namespace ImageWarping
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
