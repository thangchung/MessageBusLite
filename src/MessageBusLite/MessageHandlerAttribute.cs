using System;

namespace MessageBusLite
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class MessageHandlerAttribute : Attribute
    {
    }
}