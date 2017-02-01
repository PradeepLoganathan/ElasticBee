using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDashConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LogDashboard dash = new LogDashboard();
            dash.GetAllLogEntries();
            dash.GetByCorrelationID("64a7f771-84e6-4f25-a0c3-7b00107d6a2d");
            Console.Read();
        }
    }
}
