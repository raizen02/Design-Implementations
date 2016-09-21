using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Cti.Seller.Business.Bootstrapper;
using Cti.Seller.Business.Entities;
using Cti.Seller.Business.Managers;
using Core.Common.Core;
using SM = System.ServiceModel;
using System.Threading;
using System.Security.Principal;
using System.Transactions;

namespace Cti.Seller.ServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericPrincipal principal = new GenericPrincipal(
                new GenericIdentity("root"), new string[] { "Administrators", "SellerAdmin" });
            Thread.CurrentPrincipal = principal;

            ObjectBase.Container = MEFLoader.Init();

            Console.WriteLine("Starting up services...");
            Console.WriteLine("");

            SM.ServiceHost hostUnitInventoryManager = new SM.ServiceHost(typeof(UnitInventoryManager));
            SM.ServiceHost hostAccountManager = new SM.ServiceHost(typeof(AccountManager));
            SM.ServiceHost hostLocationManager = new SM.ServiceHost(typeof(LocationManager));


            StartService(hostUnitInventoryManager, "UnitInventoryManager");
            StartService(hostAccountManager, "AccountManager");
            StartService(hostLocationManager, "LocationManager");

            //Backend Job Sample Implementations
            //System.Timers.Timer timer = new System.Timers.Timer(10000);
            //timer.Elapsed += OnTimerElapsed;
            //timer.Start();

            //Console.WriteLine("Backend Job started.");

            Console.WriteLine("");
            Console.WriteLine("Press [Enter] to exit.");
            Console.ReadLine();
            Console.WriteLine("");

            //timer.Stop();

            //Console.WriteLine("Backend Job Ended.");

            StopService(hostUnitInventoryManager, "UnitInventoryManager");
            StopService(hostAccountManager, "AccountManager");
            StopService(hostLocationManager, "LocationManager");
        }

        //static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        //{
        //    UnitInventoryManager inventoryManager = new UnitInventoryManager();

        //    Unit[] reservations = UnitInventoryManager.ReserveUnit();
        //    if (reservations != null)
        //    {
        //        foreach (Reservation reservation in reservations)
        //        {
        //            using (TransactionScope scope = new TransactionScope())
        //            {
        //                inventoryManager.CancelReservation(reservation.ReservationId);
        //                scope.Complete();
        //            }
        //        }
        //    }
        //}

        static void StartService(SM.ServiceHost host, string serviceDescription)
        {
            host.Open();
            Console.WriteLine("Service '{0}' started.", serviceDescription);

            foreach (var endpoint in host.Description.Endpoints)
            {
                Console.WriteLine(string.Format("Listening on endpoint:"));
                Console.WriteLine(string.Format("Address: {0}", endpoint.Address.Uri.ToString()));
                Console.WriteLine(string.Format("Binding: {0}", endpoint.Binding.Name));
                Console.WriteLine(string.Format("Contract: {0}", endpoint.Contract.ConfigurationName));
            }

            Console.WriteLine();
        }

        static void StopService(SM.ServiceHost host, string serviceDescription)
        {
            host.Close();
            Console.WriteLine("Service '{0}' stopped.", serviceDescription);
        }
    }
}
