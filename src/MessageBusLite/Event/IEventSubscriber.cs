using System;
using System.Reactive;

namespace MessageBusLite.Event
{
    public interface IEventSubscriber : IDisposable
    {
        IObservable<Unit> Subscribe();
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}