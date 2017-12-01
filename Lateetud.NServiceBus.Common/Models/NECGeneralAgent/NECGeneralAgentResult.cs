
using NServiceBus;

namespace Lateetud.NServiceBus.Common.Models.NECGeneralAgent
{
    public class NECGeneralAgentResult :
        MessageType, IEvent
    {
        public string ID { get; set; }
    }
}
