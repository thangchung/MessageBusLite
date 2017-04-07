using System.Collections.Generic;
using System.Reflection;
using Autofac;
using MessageBusLite.Event;
using MessageBusLite.InMemory.Event;

namespace MessageBusLite.InMemory
{
    public static class InMemoryExtensions
    {
        public static ContainerBuilder AddInMemoryEventBus(this ContainerBuilder builder, Assembly registerAssembly)
        {
            builder.RegisterType<InMemoryPubSubSync>().As<IPubSubSync>().SingleInstance();
            builder.Register(x => new InMemoryPublisher(x.Resolve<IPubSubSync>(), "event-bus"))
                .As<IEventBus>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(registerAssembly)
                .AsImplementedInterfaces();

            builder.Register(x => new InMemorySubscriber(x.Resolve<IPubSubSync>(), "event-bus"))
                .Named<IEventSubscriber>("EventSubscriber")
                .SingleInstance();

            builder.Register(x =>
                new EventConsumer(
                    x.ResolveNamed<IEventSubscriber>("EventSubscriber"),
                    (IEnumerable<IMessageHandler>)x.Resolve(typeof(IEnumerable<IMessageHandler>)))
                ).As<IEventConsumer>()
                .SingleInstance();

            return builder;
        }
    }
}