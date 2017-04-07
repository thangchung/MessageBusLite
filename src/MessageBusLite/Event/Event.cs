namespace MessageBusLite.Event
{
    public class Event : IEvent
    {
        public byte[] Version { get; set; }
    }
}