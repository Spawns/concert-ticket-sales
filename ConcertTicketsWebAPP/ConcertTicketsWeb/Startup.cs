using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConcertTicketsWeb.Startup))]
namespace ConcertTicketsWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
