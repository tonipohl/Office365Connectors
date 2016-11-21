using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Office365ConnectorsDemo.Startup))]
namespace Office365ConnectorsDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
