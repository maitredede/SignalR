using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dede.SignalR
{
    public class SignalRScaleoutConfiguration : ScaleoutConfiguration
    {
        public string RemoteUrl { get; set; }

        public SignalRScaleoutConfiguration(string remoteUrl)
            : base()
        {
            this.RemoteUrl = remoteUrl;
        }
    }
}
