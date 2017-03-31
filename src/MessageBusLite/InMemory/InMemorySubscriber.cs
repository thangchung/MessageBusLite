using System;
using System.Reactive;
using System.Reactive.Linq;

namespace MessageBusLite.InMemory
{
    public class InMemorySubscriber : IMessageSubscriber
    {
        private readonly IPubSub _subscriber;
        private readonly string _channelName;

        public InMemorySubscriber(IPubSub subscriber, string channelName)
        {
            _subscriber = subscriber;
            _channelName = channelName;
        }

        public void Dispose()
        {
        }

        public IObservable<Unit> Subscribe()
        {
            return Observable.Start(() =>
            {
                _subscriber.Subscribe(_channelName, x =>
                {
                    OnMessageReceived(new MessageReceivedEventArgs(x));
                });
            });
        }

        private void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
    }
}