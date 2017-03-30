using System;

namespace EventBusLite
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class EventHandlerAttribute : Attribute
    {
    }
}