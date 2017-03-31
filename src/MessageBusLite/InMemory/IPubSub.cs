using System;

namespace MessageBusLite.InMemory
{
    public interface IPubSub
    {
        void Publish(string channelName, object token);
        IDisposable Subscribe(string channelName, Action<object> handler);
    }
}