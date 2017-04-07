using System;
using System.Collections.Generic;

namespace MessageBusLite.Event
{
    public interface IEventConsumer : IDisposable
    {
        IEventSubscriber Subscriber { get; }
        IEnumerable<IMessageHandler> EventHandlers { get; }
    }
}