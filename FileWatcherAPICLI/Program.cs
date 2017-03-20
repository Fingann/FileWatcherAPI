using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileWatcherAPICLI
{
    using System.Threading;

    

    using global::FileWatcherAPI.Models;

    class Program
    {
        static void Main(string[] args)
        {

            



            FileWatcherAPI Watcher = new FileWatcherAPI();
            Watcher.Events.Setup += Watcher_NewCaller;
            Watcher.Events.Alerting += Watcher_NewCaller;
            Watcher.Events.Connected += Watcher_NewCaller;
            Watcher.Events.Hangup += Watcher_NewCaller;
            Watcher.Events.NoAnswer += Watcher_NewCaller;



            Watcher.Start();
            while (true)
            {
                Thread.Sleep(100);
            }
        }

        private static void Watcher_NewCaller(object source, Call e)
        {
            if (e.catalogPhoneSearchResult != null)
            {
                if (e.catalogPhoneSearchResult.Equals("MatchFound"))
                {
                    Console.WriteLine(e.ContactFirstName_1 + ", " + e.ContactLastName_1);
                }
            }

            Console.WriteLine(e.system_call_progress);
        }
    }
}
