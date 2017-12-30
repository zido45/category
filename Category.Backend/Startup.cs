using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Category.Backend.Startup))]
namespace Category.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
