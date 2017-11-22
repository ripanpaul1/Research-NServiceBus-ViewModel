using NServiceBus;

namespace Messages
{
    public class RowMessage :
        IEvent
    {
        public string Message { get; set; }
    }
}
