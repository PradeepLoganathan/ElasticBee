using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Newtonsoft.Json;
using LogEventTypes;




namespace NotificationGenerator
{
    class GenerateNotification
    {
        public void Generator()
        {
            int ophour;
            int opmin;
            int opsec;
            Guid guid = new Guid();
            guid = Guid.NewGuid();

            ophour = 2;
            opmin = opsec = 1;

            for (int i = 0; i < 10000; i++)
            {
                if (opmin == 59)
                {
                    ophour++;
                    opmin = 0;
                }

                if (opsec == 59)
                {
                    opmin = opmin + 1;
                    opsec = 0;
                    guid = Guid.NewGuid();
                }

                opsec = opsec + 1;

                Metrics GenMetrics = new Metrics
                {
                    Uri = "http://localhost/CCS/V1/ComplexConfig/GetRuntimeSolutionStructure",
                    Description = "",
                    Message = "",
                    ActionArguments = "request = Dell.ComplexConfig.Services.Contracts.V1.Request.SolutionStructureRequest\r\n",
                    AdditionalInfo = "",
                    CorrelationId = guid.ToString(),
                    EventTimestamp = new DateTime(2017, 01, 25, 8, opmin, opsec),
                    EventType = "UserInteractionEvent",
                    HostName = "Prodbox1",
                    Key = "32cc750e-a3e6-4dbf-9a9f-5a1988351cf0",
                    SourceApplication = "CCS",
                    UserId = "pradeep_loganathan",
                    VersionNumber = "1",
                    Customer = new Customer
                    {
                        Country = "UK",
                        Language = "EN",
                        Catalog = "g_20204",
                        Region = "",
                        PageName = "GetRuntimeSolutionStructure"
                    }
                };

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Dell.SolutionSelling.Metrics.Consumer",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(GenMetrics));
                    
                    channel.BasicPublish(exchange: "",
                                         routingKey: "Dell.SolutionSelling.Metrics.Consumer",
                                         basicProperties: null,
                                         body: body);
                    System.Console.WriteLine("[x] # [{0} ] publishing message {1} into queue", i, JsonConvert.SerializeObject(GenMetrics));
                }
            }
        }
    }
}
