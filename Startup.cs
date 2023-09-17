using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(week4_database.Startup))]
namespace week4_database
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
