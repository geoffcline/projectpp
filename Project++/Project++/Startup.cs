using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project__.Startup))]
namespace Project__
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
