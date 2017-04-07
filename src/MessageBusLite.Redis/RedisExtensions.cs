using System.Collections.Generic;
using System.Reflection;
using Autofac;
using MessageBusLite.Event;
using MessageBusLite.Redis.Event;
using StackExchange.Redis;

namespace MessageBusLite.Redis
{
    public static class RedisExtensions
    {
        public static ContainerBuilder AddRedisEventBus(this ContainerBuilder builder, Assembly registerAssembly)
        {
            var options = new ConfigurationOptions
            {
                EndPoints = { "localhost" } // TODO: need to allow the configuration
            };

            var connectionMultiplexer = ConnectionMultiplexer.Connect(options);

            builder.RegisterInstance(connectionMultiplexer)
                .As<IConnectionMultiplexer>()
                .SingleInstance();

            builder.Register(x => new RedisPublisher(connectionMultiplexer.GetSubscriber(), "event-bus"))
                .As<IEventBus>()
                .SingleInstance();

            builder.RegisterAssemblyTypes(registerAssembly)
                .AsImplementedInterfaces();

            builder.RegisterInstance(new RedisSubscriber(connectionMultiplexer.GetSubscriber(), "event-bus"))
                .Named<IEventSubscriber>("EventSubscriber");

            builder.Register(x =>
                new EventConsumer(
                    x.ResolveNamed<IEventSubscriber>("EventSubscriber"),
                    (IEnumerable<IMessageHandler>)x.Resolve(typeof(IEnumerable<IMessageHandler>))
                    )
                ).As<IEventConsumer>();

            return builder;
        }
    }
}