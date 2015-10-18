using EloBuddy;
using EloBuddy.SDK.Menu;
using OneForWeek.Draw.Notifications;
using OneForWeek.Model.Notification;

namespace OneForWeek.Plugin
{
    class PluginModel
    {
        #region Global Variables

        /*
         Config
         */

        public static readonly string GVersion = "1.0.1";
        public static readonly string GCharname = _Player.ChampionName;

        /*
         Menus
         */

        public static Menu Menu,
            ComboMenu,
            LaneClearMenu,
            HarassMenu,
            MiscMenu,
            DrawMenu;

        /*
         Misc
         */

        public PluginModel()
        {
            Notification.DrawNotification(new NotificationModel(Game.Time, 0.5f, 1f, ObjectManager.Player.ChampionName + " addon loading...", System.Drawing.Color.White));
        }

        public static AIHeroClient Target;

        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }

        #endregion
    }
}
