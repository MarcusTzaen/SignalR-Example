using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Timers;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(WebApplication1.OwinStartup))]

namespace WebApplication1
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
            
        }
    }
}
