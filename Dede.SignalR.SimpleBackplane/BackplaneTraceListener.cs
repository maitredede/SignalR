using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dede.SignalR.SimpleBackplane
{
    public class BackplaneTraceListener : System.Diagnostics.TraceListener
    {
        public override void Write(string message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<BackplaneTraceHub>();
            hub.Clients.All.write(message);
        }

        public override void WriteLine(string message)
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<BackplaneTraceHub>();
            hub.Clients.All.writeLine(message);
        }
    }
}