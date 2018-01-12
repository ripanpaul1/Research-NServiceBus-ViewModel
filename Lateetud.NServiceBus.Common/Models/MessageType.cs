using NServiceBus;

namespace Lateetud.NServiceBus.Common.Models
{
    public class MessageType 
    {
        public string RequestID { get; set; }
        public string MessageID { get; set; }
        public string Message { get; set; }
    }
}
