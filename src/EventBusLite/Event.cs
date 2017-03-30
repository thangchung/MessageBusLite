namespace EventBusLite
{
    public class Event : IMessage
    {
        public byte[] Version { get; set; }
    }
}