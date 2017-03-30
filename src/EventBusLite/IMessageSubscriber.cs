using System;
using System.Reactive;

namespace EventBusLite
{
    public interface IMessageSubscriber : IDisposable
    {
        IObservable<Unit> Subscribe();
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}