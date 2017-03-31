using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reflection;

namespace MessageBusLite
{
    public class MessageConsumer : IMessageConsumer
    {
        public MessageConsumer(
            IMessageSubscriber subscriber,
            IEnumerable<IMessageHandler> eventHandlers)
        {
            Subscriber = subscriber;
            EventHandlers = eventHandlers;
            subscriber.MessageReceived += (sender, e) =>
            {
                if (EventHandlers == null) return;
                foreach (var handler in EventHandlers)
                {
                    var handlerType = handler.GetType();
                    var messageType = e.Message.GetType();
                    var methodInfoQuery =
                        from method in handlerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        let parameters = method.GetParameters()
                        where method.Name == "Handle" &&
                              method.ReturnType == typeof (IObservable<Unit>) &&
                              parameters.Length == 1 &&
                              parameters[0].ParameterType == messageType
                        select method;
                    var methodInfo = methodInfoQuery.FirstOrDefault();
                    methodInfo?.Invoke(handler, new[] {e.Message});
                }
            };
        }

        public IEnumerable<IMessageHandler> EventHandlers { get; }

        public IMessageSubscriber Subscriber { get; }

        public void Dispose()
        {
            Subscriber.Dispose();
        }
    }
}