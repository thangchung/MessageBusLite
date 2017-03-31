using System;
using System.Reactive;

namespace MessageBusLite
{
    public interface IMessageSubscriber : IDisposable
    {
        IObservable<Unit> Subscribe();
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}