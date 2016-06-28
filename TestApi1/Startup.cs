using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestApi1.Startup))]
namespace TestApi1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
