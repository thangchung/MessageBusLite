using System;
using System.Reactive;

namespace MessageBusLite
{
    public interface IMessageBusHandler<in TMessage> : IMessageHandler
        where TMessage : IMessage
    {
        IObservable<Unit> Handle(TMessage message);
    }
}