using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StripeDemo.Startup))]
namespace StripeDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
