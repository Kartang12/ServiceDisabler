using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace Disabler
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> serviceNames = new List<string>();
            ServiceController[] services = ServiceController.GetServices();
            if (!File.Exists("ServicesList.txt"))
                CreateServicesList();
            try
            {
                serviceNames = File.ReadAllLines("DisableList.txt").ToList();
            } catch (FileNotFoundException ex)
            {
                Console.WriteLine("Please create a list of services and put it to DisableList.txt each name from new line");
            }

            

            foreach (var service in services)
                if (serviceNames.Contains(service.ServiceName))
                {
                    if (service.CanStop)
                        Task.Run(() => StopTask(service));
                    else
                        Console.WriteLine("service" + service.ServiceName + "can't be stopped");
                    Console.WriteLine("\t" + "name: " + service.ServiceName + " \tdisabled: " + service.Status + "\t startMode:" + service.StartType);
                }
            Console.ReadLine();
        }

        private static void StopTask(ServiceController service)
        {
            service.Stop();
            ServiceHelper.ChangeStartMode(service, ServiceStartMode.Disabled);
        }

        private static void CreateServicesList()
        {
            ServiceController[] services = ServiceController.GetServices();
            using (StreamWriter sw = File.CreateText("ServicesList.txt"))
            {
                foreach(var service in services)
                {
                    sw.WriteLine(service.ServiceName + " - " + service.DisplayName);
                }
            }
        }
    }
}
