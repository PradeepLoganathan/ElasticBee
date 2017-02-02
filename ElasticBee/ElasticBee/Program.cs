using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ElasticBee
{
    class Program
    {
        static void Main(string[] args)
        {
            LogConsumer Consumer = new LogConsumer();
            Consumer.DoWork();
         }
    }
}
