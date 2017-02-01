using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateNotification Gennotify = new GenerateNotification();
            Gennotify.Generator();

        }
    }
}
