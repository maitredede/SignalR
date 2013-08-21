using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dede.SignalR
{
    /// <summary>
    /// SignalR message bus backplane
    /// </summary>
    public sealed class SignalRMessageBus : ScaleoutMessageBus
    {
        private readonly HubConnection m_hub;
        private readonly IHubProxy m_proxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="SignalRMessageBus"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <param name="config">The configuration.</param>
        public SignalRMessageBus(IDependencyResolver resolver, SignalRScaleoutConfiguration config)
            : base(resolver, config)
        {
            this.m_hub = new HubConnection(config.RemoteUrl);
            this.m_hub.Error += this.m_hub_Error;
            this.m_proxy = this.m_hub.CreateHubProxy("backplaneHub");
            this.m_proxy.On<int, long, byte[]>("send", this.Received);
            this.m_hub.Start().Wait();
            base.Open(0);
        }

        private void m_hub_Error(Exception obj)
        {
            throw new NotImplementedException("MessageBusErrorHandling", obj);
        }

        private void Received(int streamIndex, long id, byte[] data)
        {
            ScaleoutMessage message = ScaleoutMessage.FromBytes(data);
            ulong uid = unchecked((ulong)id);
            lock (typeof(SignalRMessageBus))
            {
                base.OnReceived(streamIndex, uid, message);
            }
        }

        protected override Task Send(int streamIndex, IList<Message> messages)
        {
            return this.m_proxy.Invoke("send", streamIndex, new ScaleoutMessage(messages).ToBytes());

        }
    }
}
