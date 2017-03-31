using System;
using System.Reactive;
using System.Reactive.Linq;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MessageBusLite.Redis
{
    public class RedisPublisher : IDisposable, IMessageBus
    {
        private readonly ISubscriber _subscriber;
        private readonly string _channelName;

        public RedisPublisher(ISubscriber subscriber, string channelName)
        {
            _subscriber = subscriber;
            _channelName = channelName;
        }

        public void Dispose()
        {
            _subscriber.UnsubscribeAsync(_channelName);
        }

        public IObservable<Unit> Publish<T>(T @event)
        {
            return Observable.Start(() =>
            {
                var json = JsonConvert.SerializeObject(
                    @event,
                    Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                _subscriber.Publish(_channelName, json);
            });
        }
    }
}