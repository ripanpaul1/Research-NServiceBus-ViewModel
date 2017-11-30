
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
        private double cacheFor { get; set; }
        #endregion

        #region MsmqSqlDBConfiguration

        #region MsmqSqlDBConfiguration [1]
        public MsmqSqlDBConfiguration(string connectionString)
        {
            this.connectionString = connectionString;
            this.cacheFor = 1;
        }
        #endregion

        #region MsmqSqlDBConfiguration [2]
        public MsmqSqlDBConfiguration(string connectionString, double cacheFor)
        {
            this.connectionString = connectionString;
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

            await Task.CompletedTask;
        }
        #endregion

        #endregion

        #region Public Methods

        #region ConfigureEndpoint

        #region ConfigureEndpoint [1]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName)
        {
            return this.ConfigureEndpoint(EndpointName, this.cacheFor, null);
        }
        #endregion

        #region ConfigureEndpoint [2]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, double cacheFor)
        {
            return this.ConfigureEndpoint(EndpointName, cacheFor, null);
        }
        #endregion

        #region ConfigureEndpoint [3]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, List<PublisherEndpoints> publisherEndpoints)
        {
            return this.ConfigureEndpoint(EndpointName, this.cacheFor, publisherEndpoints);
        }
        #endregion

        #region ConfigureEndpoint [4]
        public EndpointConfiguration ConfigureEndpoint(string EndpointName, double cacheFor, List<PublisherEndpoints> publisherEndpoints)
        {
            var endpointConfiguration = new EndpointConfiguration(EndpointName);
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.SendFailedMessagesTo("error");

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

        #region StartEndpoint
        public async Task StartEndpoint(EndpointConfiguration endpointConfiguration)
        {
            await Endpoint.Start(endpointConfiguration)
                        .ConfigureAwait(false);
        }
        #endregion

        #region PublishedToBus
        public string PublishedToBus(EndpointConfiguration endpointConfiguration, object model)
        {
            try
            {
                MessagePublished(endpointConfiguration, model).GetAwaiter().GetResult();
                return "Published";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #endregion

    }
}
