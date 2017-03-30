using System;

namespace EventBusLite.InMemory
{
    public interface IPubSub
    {
        void Publish(string channelName, object token, bool notifySelf);
        IDisposable Subscribe(string channelName, Action<object> handler);
    }
}