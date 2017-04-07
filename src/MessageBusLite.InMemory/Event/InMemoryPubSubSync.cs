using System;
using System.Collections.Concurrent;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace MessageBusLite.InMemory.Event
{
    public class InMemoryPubSubSync : IPubSubSync
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

        public Task<object> Send(string channelName, object token)
        {
            object temp = null;
            _subjects.GetOrAdd(channelName, k =>
            {
                var ret = new Subject<object>();
                ret.Subscribe(o => temp = o);
                return ret;
            }).OnNext(token);
            return Task.FromResult(temp);
        }
    }
}