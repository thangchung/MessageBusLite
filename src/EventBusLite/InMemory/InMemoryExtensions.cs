using System.Collections.Generic;
using System.Reflection;
using Autofac;

namespace EventBusLite.InMemory
{
    public static class InMemoryExtensions
    {
        public static ContainerBuilder AddInMemoryEventBus(this ContainerBuilder builder, Assembly registerAssembly)
        {
            builder.RegisterType<InMemoryPubSub>().As<IPubSub>().SingleInstance();
            builder.Register(x => new InMemoryPublisher(x.Resolve<IPubSub>(), "notify"))
                .As<IEventBus>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(registerAssembly)
                .AsImplementedInterfaces();

            builder.Register(x => new InMemorySubscriber(x.Resolve<IPubSub>(), "notify"))
                .Named<IMessageSubscriber>("EventSubscriber")
                .SingleInstance();

            builder.Register(x =>
                new EventConsumer(
                    x.ResolveNamed<IMessageSubscriber>("EventSubscriber"),
                    (IEnumerable<IEventHandler>)x.Resolve(typeof(IEnumerable<IEventHandler>))
                    )
                ).As<IEventConsumer>();

            return builder;
        }
    }
}