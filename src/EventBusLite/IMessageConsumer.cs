using System;

namespace EventBusLite
{
    public interface IMessageConsumer : IDisposable
    {
        IMessageSubscriber Subscriber { get; }
    }
}