using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;
using LogEventTypes;

namespace ElasticBee
{
    class LinnaeusApis
    {
        private Uri ElasticUri;
        private ConnectionSettings connectionsettings;
        private ElasticClient elasticclient;

        private ConnectionFactory factory;
        private IConnection connection;
        private IModel channel;


        public LinnaeusApis()
        {
            

        }

        public void DoWork()
        {
            ConnectToElastic();
            SetupElastic();
            ConnectToRabbit();
            ReadFromRabbit();
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
            connectionsettings.DefaultIndex("solutionselling.metrics.write");
            elasticclient = new ElasticClient(connectionsettings);
        }

        private void SetupElastic()
        {
            var settings = new IndexSettings();
            settings.NumberOfReplicas = 1;
            settings.NumberOfShards = 5;
            var response = elasticclient.IndexExists(new IndexExistsRequest("solutionselling.metrics.write"));
            if(!response.Exists)
            {
                var result = elasticclient.CreateIndex("solutionselling.metrics.write");
                if (!result.IsValid)
                    Console.WriteLine(result.OriginalException.Message);
            }
        }


        private void ReadFromRabbit()
        {
            channel.QueueDeclare(queue: "Dell.SolutionSelling.Metrics.Consumer",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                LogEventTypes.Metrics genmetrics = JsonConvert.DeserializeObject<LogEventTypes.Metrics>(message);
                WriteToElastic(genmetrics);
                Console.WriteLine(" [x] Received {0}", message);
            };

            channel.BasicConsume(queue: "Dell.SolutionSelling.Metrics.Consumer",
                                 noAck: true,
                                 consumer: consumer);
        }

        private void WriteToElastic(LogEventTypes.Metrics genmetrics)
        {
            var index = elasticclient.Index(genmetrics);
        }



    }


}
