using System;
using System.Reactive;

namespace EventBusLite
{
    public interface IEventBus
    {
        IObservable<Unit> Publish<T>(T @event);
    }
}