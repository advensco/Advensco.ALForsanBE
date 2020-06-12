using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Advensco.ALForsanBE.Startup))]
namespace Advensco.ALForsanBE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
