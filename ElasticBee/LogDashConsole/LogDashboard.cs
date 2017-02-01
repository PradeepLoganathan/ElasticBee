using System;
using Nest;

namespace LogDashConsole
{
    class LogDashboard
    {
        private Uri ElasticUri;
        private ConnectionSettings connectionsettings;
        private ElasticClient elasticclient;

        public LogDashboard()
        {
            ConnectToElastic();
        }

        private void ConnectToElastic()
        {
            ElasticUri = new Uri("http://localhost:9200");
            connectionsettings = new ConnectionSettings(ElasticUri);
            connectionsettings.DefaultIndex("solutionselling.metrics");
            elasticclient = new ElasticClient(connectionsettings);
        }

        public void GetAllLogEntries()
        {
            var res = elasticclient.Search<LogEventTypes.Metrics>(s => s
            .From(0)
            .Size(25)
            .Query(q => q.MatchAll()));

            foreach (LogEventTypes.Metrics m in res.Documents)
            {
                Console.WriteLine(m.CorrelationId.ToString());
            }
        }
        public void GetByCorrelationID(string CorrelationID)
        {
            var res = elasticclient.Search<LogEventTypes.Metrics>(s => s
            .From(0)
            .Query(q => q.MatchPhrase(c => c
                                      .Field(f => f.CorrelationId)
                                      .Query(CorrelationID)
                                      )));


            foreach (LogEventTypes.Metrics m in res.Documents)
            {
                Console.WriteLine(m.CorrelationId.ToString());
            }

        }


    }
}
