using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dede.SignalR
{
    public static class DependencyResolverExtensions
    {
        public static IDependencyResolver UseDede(this IDependencyResolver resolver, string url)
        {
            SignalRScaleoutConfiguration config = new SignalRScaleoutConfiguration(url);
            return UseDede(resolver, config);
        }

        public static IDependencyResolver UseDede(this IDependencyResolver resolver, SignalRScaleoutConfiguration configuration)
        {
            var bus = new Lazy<SignalRMessageBus>(() => new SignalRMessageBus(resolver, configuration));
            resolver.Register(typeof(IMessageBus), () => bus.Value);

            return resolver;
        }
    }
}
