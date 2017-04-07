using System;
using System.Reactive;
using System.Reactive.Linq;
using MessageBusLite.Event;

namespace MessageBusLite.InMemory.Event
{
    public class InMemoryPublisher : IEventBus
    {
        private readonly IPubSubSync _subscriber;
        private readonly string _channelName;

        public InMemoryPublisher(IPubSubSync subscriber, string channelName)
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