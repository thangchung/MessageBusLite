using System.Collections.Generic;
using System.Reflection;
using Autofac;

namespace MessageBusLite.InMemory
{
    public static class InMemoryExtensions
    {
        public static ContainerBuilder AddInMemoryEventBus(this ContainerBuilder builder, Assembly registerAssembly)
        {
            builder.RegisterType<InMemoryPubSub>().As<IPubSub>().SingleInstance();
            builder.Register(x => new InMemoryPublisher(x.Resolve<IPubSub>(), "notify"))
                .As<IMessageBus>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(registerAssembly)
                .AsImplementedInterfaces();

            builder.Register(x => new InMemorySubscriber(x.Resolve<IPubSub>(), "notify"))
                .Named<IMessageSubscriber>("EventSubscriber")
                .SingleInstance();

            builder.Register(x =>
                new MessageConsumer(
                    x.ResolveNamed<IMessageSubscriber>("EventSubscriber"),
                    (IEnumerable<IMessageHandler>)x.Resolve(typeof(IEnumerable<IMessageHandler>))
                    )
                ).As<IMessageConsumer>()
                .SingleInstance();

            return builder;
        }
    }
}