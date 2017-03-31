using System;
using System.Reactive;
using System.Reactive.Linq;

namespace MessageBusLite.InMemory
{
    public class InMemoryPublisher : IMessageBus
    {
        private readonly IPubSub _subscriber;
        private readonly string _channelName;

        public InMemoryPublisher(IPubSub subscriber, string channelName)
        {
            _subscriber = subscriber;
            _channelName = channelName;
        }

        public IObservable<Unit> Publish<T>(T @event)
        {
            return Observable.Start(() =>
            {
                _subscriber.Publish(_channelName, @event);
            });
        }
    }
}