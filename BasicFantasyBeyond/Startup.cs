using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BasicFantasyBeyond.Startup))]
namespace BasicFantasyBeyond
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
