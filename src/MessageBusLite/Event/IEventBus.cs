using System;
using System.Reactive;

namespace MessageBusLite.Event
{
    public interface IEventBus
    {
        IObservable<Unit> Publish<T>(T message);
    }
}