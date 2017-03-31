using System;
using System.Reactive;

namespace MessageBusLite
{
    public interface IMessageBus
    {
        IObservable<Unit> Publish<T>(T message);
    }
}