
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NServiceBus;
using NServiceBus.Persistence.Sql;
using System.Threading.Tasks;

namespace Lateetud.NServiceBus.Common
{
    public class MsmqSqlDBConfiguration
    {
        IEndpointInstance endpointInstance = null;

        #region Local Property
        private string connectionString { get; set; }
        private string errorQueue { get; set; }
        private double cacheFor { get; set; }
        private int numberOfRetries { get; set; }
        private int timeIncrease { get; set; }
        #endregion

        #region MsmqSqlDBConfiguration

        #region MsmqSqlDBConfiguration [1]
        public MsmqSqlDBConfiguration(string connectionString)
        {
            this.connectionString = connectionString;
            this.errorQueue = "error";
            this.numberOfRetries = 5;
            this.timeIncrease = 10;
            this.cacheFor = 1;
        }
        #endregion

        #region MsmqSqlDBConfiguration [2]
        public MsmqSqlDBConfiguration(string connectionString, string errorQueue)
        {
            this.connectionString = connectionString;
            this.errorQueue = errorQueue;
            this.numberOfRetries = 5;
            this.timeIncrease = 10;
            this.cacheFor = 1;
        }
        #endregion

        #region MsmqSqlDBConfiguration [3]
        public MsmqSqlDBConfiguration(string connectionString, int numberOfRetries, int timeIncrease)
        {
            this.connectionString = connectionString;
            this.errorQueue = "error";
            this.numberOfRetries = numberOfRetries;
            this.timeIncrease = timeIncrease;
            this.cacheFor = 1;
        }
        #endregion

        #region MsmqSqlDBConfiguration [4]
        public MsmqSqlDBConfiguration(string connectionString, string errorQueue, int numberOfRetries, int timeIncrease)
        {
            this.connectionString = connectionString;
            this.errorQueue = errorQueue;
            this.numberOfRetries = numberOfRetries;
            this.timeIncrease = timeIncrease;
            this.cacheFor = 1;
        }
        #endregion

        #region MsmqSqlDBConfiguration [5]
        public MsmqSqlDBConfiguration(string connectionString, double cacheFor)
        {
            this.connectionString = connectionString;
            this.errorQueue = "error";
            this.numberOfRetries = 5;
            this.timeIncrease = 10;
            this.cacheFor = cacheFor;
        }
        #endregion

        #region MsmqSqlDBConfiguration [6]
        public MsmqSqlDBConfiguration(string connectionString, string errorQueue, double cacheFor)
        {
            this.connectionString = connectionString;
            this.errorQueue = errorQueue;
            this.numberOfRetries = 5;
            this.timeIncrease = 10;
            this.cacheFor = cacheFor;
        }
        #endregion

        #region MsmqSqlDBConfiguration [7]
        public MsmqSqlDBConfiguration(string connectionString, int numberOfRetries, int timeIncrease, double cacheFor)
        {
            this.connectionString = connectionString;
            this.errorQueue = "error";
            this.numberOfRetries = numberOfRetries;
            this.timeIncrease = timeIncrease;
            this.cacheFor = cacheFor;
        }
        #endregion

        #region MsmqSqlDBConfiguration [8]
        public MsmqSqlDBConfiguration(string connectionString, string errorQueue, int numberOfRetries, int timeIncrease, double cacheFor)
        {
            this.connectionString = connectionString;
            this.errorQueue = errorQueue;
            this.numberOfRetries = numberOfRetries;
            this.timeIncrease = timeIncrease;
            this.cacheFor = cacheFor;
        }
        #endregion

        #endregion

        #region Private Methods

        #region MessagePublished
        private async Task MessagePublished(EndpointConfiguration endpointConfiguration, object model)
        {
            endpointInstance = endpointInstance ?? await Endpoint.Start(endpointConfiguration)
                        .ConfigureAwait(false);

            await endpointInstance.Publish(model)
                .ConfigureAwait(false);

            await endpointInstance.Stop()
                        .ConfigureAwait(false);
            endpointInstance = null;

            await Task.CompletedTask;
        }
        #endregion

        #endregion

        #region Public Methods

        #region IsFailedQ
        public bool IsFailedQ(IMessageHandlerContext context, string messageHeaderKey)
        {
            foreach (var messageHeader in context.MessageHeaders)
                if (messageHeader.Key == messageHeaderKey) return true;
            return false;
        }
        #endregion

        #region ConfigureEndpoint

        #region ConfigureEndpoint [1]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName)
        {
            return this.ConfigureEndpoint(EndpointName, this.errorQueue, this.cacheFor, null);
        }
        #endregion

        #region ConfigureEndpoint [2]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, string errorQueue)
        {
            return this.ConfigureEndpoint(EndpointName, errorQueue, this.cacheFor, null);
        }
        #endregion

        #region ConfigureEndpoint [3]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, double cacheFor)
        {
            return this.ConfigureEndpoint(EndpointName, this.errorQueue, cacheFor, null);
        }
        #endregion

        #region ConfigureEndpoint [4]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, string errorQueue, double cacheFor)
        {
            return this.ConfigureEndpoint(EndpointName, errorQueue, cacheFor, null);
        }
        #endregion

        #region ConfigureEndpoint [5]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, List<PublisherEndpoints> publisherEndpoints)
        {
            return this.ConfigureEndpoint(EndpointName, this.errorQueue, this.cacheFor, publisherEndpoints);
        }
        #endregion

        #region ConfigureEndpoint [6]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, string errorQueue, List<PublisherEndpoints> publisherEndpoints)
        {
            return this.ConfigureEndpoint(EndpointName, errorQueue, this.cacheFor, publisherEndpoints);
        }
        #endregion

        #region ConfigureEndpoint [7]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, double cacheFor, List<PublisherEndpoints> publisherEndpoints)
        {
            return this.ConfigureEndpoint(EndpointName, this.errorQueue, this.cacheFor, publisherEndpoints);
        }
        #endregion

        #region ConfigureEndpoint [8]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, string errorQueue, double cacheFor, List<PublisherEndpoints> publisherEndpoints)
        {
            var endpointConfiguration = new EndpointConfiguration(EndpointName);
            endpointConfiguration.EnableInstallers();
            if (!string.IsNullOrWhiteSpace(errorQueue)) endpointConfiguration.SendFailedMessagesTo(errorQueue);

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.Delayed(
                customizations: delayed =>
                {
                    delayed.NumberOfRetries(this.numberOfRetries);
                    delayed.TimeIncrease(TimeSpan.FromSeconds(this.timeIncrease));
                });

            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            persistence.SqlVariant(SqlVariant.MsSqlServer);
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(this.connectionString);
                });
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.DisableCache();

            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            if (publisherEndpoints != null && publisherEndpoints.Count > 0)
            {
                var routing = transport.Routing();
                foreach (PublisherEndpoints endpoint in publisherEndpoints)
                {
                    routing.RegisterPublisher(
                        eventType: endpoint.MessageType,
                        publisherEndpoint: endpoint.EndpointName);
                }
            }
            return endpointConfiguration;
        }
        #endregion

        #endregion

        #region CreateEndpointInitializePipeline
        public async Task CreateEndpointInitializePipeline(EndpointConfiguration endpointConfiguration)
        {
            await Endpoint.Start(endpointConfiguration)
                        .ConfigureAwait(false);
        }

        //public IMessageSession CreateEndpointInitializePipeline(EndpointConfiguration endpointConfiguration)
        //{
        //    var messageSession = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        //    return messageSession;
        //}
        #endregion

        #region PublishedToBus
        public string PublishedToBus(EndpointConfiguration endpointConfiguration, object model)
        {
            MessagePublished(endpointConfiguration, model).GetAwaiter().GetResult();
            return "Published";
        }
        #endregion

        #endregion

    }
}
