using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using RabbitMQ.Client;

namespace ElasticBee
{
    class LinnaeusApis
    {
        private Uri ElasticUri;
        private ConnectionSettings connectionsettings;
        private ElasticClient client;

        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;


        public LinnaeusApis()
        {
            ConnectToElastic();
            ConnectToRabbit();
        }

        private void ConnectToRabbit()
        {
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        private void ConnectToElastic()
        {
            ElasticUri = new Uri("http://localhost:9200");
            connectionsettings = new ConnectionSettings(ElasticUri);
            client = new ElasticClient(connectionsettings);
        }
    }


}
