using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dede.SignalR
{
    public class ChatHub : Hub
    {
        public void Send(string msg)
        {
            this.Clients.All.message(msg);
        }
    }
}