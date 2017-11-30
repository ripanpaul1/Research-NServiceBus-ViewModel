using NServiceBus;

namespace Lateetud.NServiceBus.Common
{
    public class TestMessage :
        IEvent
    {
        public string Message { get; set; }
    }
}
