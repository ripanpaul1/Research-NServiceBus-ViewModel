using NServiceBus;

namespace Lateetud.NServiceBus.Common
{
    public class RowMessage :
        IEvent
    {
        public string Message { get; set; }
    }
}
