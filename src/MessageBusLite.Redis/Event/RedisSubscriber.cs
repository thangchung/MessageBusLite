using System;
using System.Reactive;
using System.Reactive.Linq;
using MessageBusLite.Event;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MessageBusLite.Redis.Event
{
    public class RedisSubscriber : IEventSubscriber
    {
        private readonly string _channelName;
        private readonly ISubscriber _subscriber;

        public RedisSubscriber(ISubscriber subscriber, string channelName)
        {
            _subscriber = subscriber;
            _channelName = channelName;
        }

        public void Dispose()
        {
            _subscriber.UnsubscribeAsync(_channelName);
        }

        public IObservable<Unit> Subscribe()
        {
            return Observable.Start(() =>
            {
                _subscriber.Subscribe(_channelName, (_, message) =>
                {
                    var obj = JsonConvert.DeserializeObject(
                        message,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        });
                    OnMessageReceived(new MessageReceivedEventArgs(obj));
                });
            });
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        private void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }
    }
}