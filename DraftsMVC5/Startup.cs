using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DraftsMVC5.Startup))]
namespace DraftsMVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
