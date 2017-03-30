using System;

namespace EventBusLite
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(object message)
        {
            Message = message;
        }

        public object Message { get; set; }
    }
}