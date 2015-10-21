using System;
using System.Threading;
using EloBuddy.SDK.Events;
using RealTimeInfo.Model.Controller;

namespace RealTimeInfo
{
    class Program
    {
        public static bool run = true;
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadCompleted;
        }

        private static void OnLoadCompleted(EventArgs args)
        {
            Startthread();
        }

        private static void Startthread()
        {
            var thread = new Thread(() =>
            {
                AsynchronousSocketListener.StartSocketServer();
                while (run)
                {
                    Console.WriteLine("Server Running");
                    Thread.Sleep(20000);
                }
            });

            thread.Start();
        }
    }
}
