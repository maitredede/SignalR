using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Dede.SignalR.SimpleBackplane
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            HubConfiguration config = new HubConfiguration()
            {
                EnableCrossDomain = true,
                EnableDetailedErrors = true,
                EnableJavaScriptProxies = true,
            };
            RouteTable.Routes.MapHubs(config);

            var traceManager = GlobalHost.DependencyResolver.Resolve<Microsoft.AspNet.SignalR.Tracing.ITraceManager>();
            traceManager.Switch.Level = SourceLevels.Verbose;
        }
    }
}