using System.Collections.Generic;
using System.Reflection;
using Autofac;
using StackExchange.Redis;

namespace EventBusLite.Redis
{
    public static class RedisExtensions
    {
        public static ContainerBuilder AddRedisEventBus(this ContainerBuilder builder, Assembly registerAssembly)
        {
            var options = new ConfigurationOptions
            {
                EndPoints = { "localhost" }
            };

            var connectionMultiplexer = ConnectionMultiplexer.Connect(options);

            builder.RegisterInstance(connectionMultiplexer)
                .As<IConnectionMultiplexer>()
                .SingleInstance();

            builder.Register(x => new RedisPublisher(connectionMultiplexer.GetSubscriber(), "notify"))
                .As<IEventBus>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(registerAssembly)
                .AsImplementedInterfaces();

            builder.RegisterInstance(new RedisSubscriber(connectionMultiplexer.GetSubscriber(), "notify"))
                .Named<IMessageSubscriber>("EventSubscriber");

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