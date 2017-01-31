using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticBee
{

    #region jsonclass

    //Dell.SolutionSelling
    public class Metrics
    {
        public string Uri { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string ActionArguments { get; set; }
        public string AdditionalInfo { get; set; }
        public string CorrelationId { get; set; }
        public DateTime EventTimestamp { get; set; }
        public string EventType { get; set; }
        public string HostName { get; set; }
        public string Key { get; set; }
        public string SourceApplication { get; set; }
        public string UserId { get; set; }
        public string VersionNumber { get; set; }
        public Customer Customer { get; set; }
    }

    public class Customer
    {
        public string Country { get; set; }
        public string Language { get; set; }
        public string Catalog { get; set; }
        public string Region { get; set; }
        public string PageName { get; set; }
    }

    #endregion
}