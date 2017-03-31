using System;
using System.Collections.Generic;

namespace MessageBusLite
{
    public interface IMessageConsumer : IDisposable
    {
        IMessageSubscriber Subscriber { get; }
        IEnumerable<IMessageHandler> EventHandlers { get; }
    }
}