﻿using System;
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

            serviceNames = File.ReadAllLines("DisableList.txt").ToList();

            

            foreach (var service in services)
                if (serviceNames.Contains(service.ServiceName))
                {
                    //if (service.CanStop)
                    //{
                        Task.Run(() => StopTask(service));
                    //}
                    Console.WriteLine("\t" + "name: " + service.ServiceName + " \tdisabled: " + service.Status + "\t startMode:" + service.StartType);
                }
            Console.ReadLine();
        }

        static void StopTask(ServiceController service)
        {

            int worker = 0;

            int available = 0;

            ThreadPool.GetAvailableThreads(out worker, out available);

            //Console.WriteLine(service.ServiceName + " - " + service.StartType);
            //service.Stop();
            //ServiceHelper.ChangeStartMode(service, ServiceStartMode.Disabled);
            Console.WriteLine("worker: " + worker + " availdable:" + available);
            Thread.Sleep(2000);
        }
    }
}
