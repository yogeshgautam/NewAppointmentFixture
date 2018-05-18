using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppointmentFixturesProject.Startup))]
namespace AppointmentFixturesProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
