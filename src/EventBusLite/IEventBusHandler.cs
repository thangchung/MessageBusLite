using System;
using System.Reactive;

namespace EventBusLite
{
    public interface IEventBusHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        IObservable<Unit> Handle(TEvent message);
    }
}