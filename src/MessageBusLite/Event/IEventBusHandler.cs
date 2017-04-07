using System;
using System.Reactive;

namespace MessageBusLite.Event
{
    public interface IEventBusHandler<in TMessage> : IMessageHandler
        where TMessage : IMessage
    {
        IObservable<Unit> Handle(TMessage message);
    }
}