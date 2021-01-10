using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;

namespace Disabler
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> serviceNames = new List<string>();
            ServiceController[] services = ServiceController.GetServices();

            serviceNames = File.ReadAllLines("DisableList.txt").ToList();

            foreach (var service in services)
                if (serviceNames.Contains(service.ServiceName))
                {
                    if (service.CanStop)
                    {
                        Console.WriteLine(service.ServiceName + " - " + service.StartType);
                        service.Stop();
                        ServiceHelper.ChangeStartMode(service, ServiceStartMode.Disabled);
                        //Thread.Sleep(3000);
                    }
                    Console.WriteLine("\t" + "name: " + service.ServiceName + " \tdisabled: " + service.Status + "\t startMode:" + service.StartType);
                }
            Console.ReadLine();
        }
    }
}
