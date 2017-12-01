using System;

namespace Lateetud.NServiceBus.Common
{
    public class PublisherEndpoints
    {
        public string EndpointName { get; set; }
        public Type MessageType { get; set; }
        public string MessageTypeNamespace { get; set; }
        public PublisherEndpoints(string endpointName, Type messageType)
        {
            EndpointName = endpointName;
            MessageType = messageType;
            MessageTypeNamespace = null;
        }

        public PublisherEndpoints(string endpointName, Type messageType, string messageTypeNamespace)
        {
            EndpointName = endpointName;
            MessageType = messageType;
            MessageTypeNamespace = messageTypeNamespace;
        }
    }
}
