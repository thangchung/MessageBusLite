using System.Collections.Generic;

namespace EventBusLite
{
    public interface IEventConsumer : IMessageConsumer
    {
        IEnumerable<IEventHandler> EventHandlers { get; }
    }
}