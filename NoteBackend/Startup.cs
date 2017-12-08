using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(NoteBackend.Startup))]

namespace NoteBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}