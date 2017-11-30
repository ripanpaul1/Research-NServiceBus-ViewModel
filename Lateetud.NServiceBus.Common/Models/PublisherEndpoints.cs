using System;

namespace Lateetud.NServiceBus.Common
{
    public class PublisherEndpoints
    {
        public string EndpointName { get; set; }
        public Type MessageType { get; set; }
        public PublisherEndpoints(string endpointName, Type messageType)
        {
            EndpointName = endpointName;
            MessageType = messageType;
        }
    }
}
