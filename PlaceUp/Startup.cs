using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlaceUp.Startup))]
namespace PlaceUp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
