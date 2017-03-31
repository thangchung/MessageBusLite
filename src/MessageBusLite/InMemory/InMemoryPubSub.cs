using System;
using System.Collections.Concurrent;
using System.Reactive.Subjects;

namespace MessageBusLite.InMemory
{
    public class InMemoryPubSub : IPubSub
    {
        private readonly ConcurrentDictionary<string, Subject<object>> _subjects =
            new ConcurrentDictionary<string, Subject<object>>();

        public void Publish(string channelName, object token)
        {
            _subjects.GetOrAdd(channelName, k => new Subject<object>()).OnNext(token);
        }

        public IDisposable Subscribe(string channelName, Action<object> handler)
        {
            return _subjects.GetOrAdd(channelName, k => new Subject<object>()).Subscribe(handler);
        }
    }
}