using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Tracing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dede.SignalR
{
    /// <summary>
    /// SignalR backplane hub
    /// </summary>
    public class BackplaneHub : Hub
    {
        /// <summary>
        /// UniqueId of messages
        /// </summary>
        private static long s_nextId = long.MinValue;
        /// <summary>
        /// Trace
        /// </summary>
        private static readonly TraceSource s_trace;

        static BackplaneHub()
        {
            var traceManager = GlobalHost.DependencyResolver.Resolve<ITraceManager>();
            s_trace = traceManager[typeof(BackplaneHub).FullName];
        }

        public override Task OnConnected()
        {
            s_trace.TraceVerbose("Client connected");
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            s_trace.TraceVerbose("Client disconnected");
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            s_trace.TraceVerbose("Client reconnected");
            return base.OnReconnected();
        }

        public void Send(int streamIndex, byte[] data)
        {
            s_trace.TraceVerbose("Send message");
            var id = Interlocked.Increment(ref s_nextId);
            this.Clients.All.Send(streamIndex, id, data);
        }
    }
}
