namespace MessageBusLite
{
    public class Event : IMessage
    {
        public byte[] Version { get; set; }
    }
}