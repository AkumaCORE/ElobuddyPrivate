using System;
using System.Drawing;
using EloBuddy;
using EloBuddy.SDK.Events;
using OneForWeek.Draw.Notifications;
using OneForWeek.Model.Notification;
using OneForWeek.Plugin;
using OneForWeek.Plugin.Hero;

namespace OneForWeek
{
    class Program
    {
        public static PluginModel Champion;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += OnLoadCompleted;
        }

        private static void OnLoadCompleted(EventArgs args)
        {
            try
            {
                /*
                var handle = System.Reflection.Assembly.GetExecutingAssembly().CreateInstance("OneForWeek.Plugin.Hero." + ObjectManager.Player.ChampionName);
                Champion = (PluginModel)handle;
                Assembly assembly = Assembly.GetExecutingAssembly();
                MsgPrinter printer = assembly.CreateInstance("Test.MsgPrinter") as MsgPrinter;
                */
                var handle = Activator.CreateInstance(null, "OneForWeek.Plugin.Hero." + ObjectManager.Player.ChampionName);
                Champion = (PluginModel)handle.Unwrap();
            }
            catch (Exception)
            {
                Notification.DrawNotification(new NotificationModel(Game.Time, 20f, 1f, ObjectManager.Player.ChampionName + " is Not Supported", Color.Red));
            }
        }
    }
}
