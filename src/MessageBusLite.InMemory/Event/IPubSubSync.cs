using System;
using System.Threading.Tasks;

namespace MessageBusLite.InMemory.Event
{
    public interface IPubSubSync
    {
        void Publish(string channelName, object token);
        IDisposable Subscribe(string channelName, Action<object> handler);
        Task<object> Send(string channelName, object token);
    }
}