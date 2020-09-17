using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CourseASP.NET.Startup))]
namespace CourseASP.NET
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
