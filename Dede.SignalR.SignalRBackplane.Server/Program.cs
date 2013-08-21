using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;

namespace Dede.SignalR
{
    class Program
    {
        static void Main(string[] args)
        {
            // This will *ONLY* bind to localhost, if you want to bind to all addresses
            // use http://*:8080 to bind to all addresses. 
            // See http://msdn.microsoft.com/en-us/library/system.net.httplistener.aspx for more info
            string url = "http://*:9630";

            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Turn cross domain on 
            HubConfiguration config = new HubConfiguration
            {
                EnableCrossDomain = true,
#if DEBUG
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = true,
#endif
            };

            // This will map out to http://*:9630/signalr by default
            app.MapHubs(config);
        }
    }

}
