using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProbMVC.Startup))]
namespace ProbMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
