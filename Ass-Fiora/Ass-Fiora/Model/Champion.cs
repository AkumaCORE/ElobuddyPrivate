using System;
using System.Drawing;
using Ass_Fiora.Controller;
using BRSelector;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using OneForWeek.Draw.Notifications;
using OneForWeek.Model.Notification;
using OneForWeek.Util.Misc;

namespace Ass_Fiora.Model
{
    class Champion : PluginModel
    {
        public new EnumModeManager ActiveMode { get; set; }

        public override void Init()
        {
            InitVariables();
            InitEvents();
            ModeManager.Initialize();
            Notification.DrawNotification(new NotificationModel(Game.Time, 5f, 1f, Player.Instance.ChampionName + " Loaded.", Color.Green));
        }

        public override void InitVariables()
        {
            Selector.Init();

            Q = new Spell.Skillshot(SpellSlot.Q, 600, SkillShotType.Circular, 250, int.MaxValue);
            W = new Spell.Skillshot(SpellSlot.W, 750, SkillShotType.Linear, 500, int.MaxValue);
            E = new Spell.Active(SpellSlot.E, 200);
            R = new Spell.Targeted(SpellSlot.R, 550);

            InitMenu();
        }

        public override void InitEvents()
        {
            Drawing.OnDraw += OnDraw;
            Orbwalker.OnPostAttack += OnAfterAttack;
        }

        public override void InitMenu()
        {
            Menu = MainMenu.AddMenu(GCharname, GCharname);

            Menu.AddLabel("Version: " + GVersion);
            Menu.AddSeparator();
            Menu.AddLabel("By Vector");

            DrawMenu = Menu.AddSubMenu("Draw - " + GCharname, GCharname + "Draw");
            DrawMenu.AddGroupLabel("Draw");
            DrawMenu.Add("drawDisable", new CheckBox("Turn off all drawings", true));
            DrawMenu.Add("drawQ", new CheckBox("Draw Q Range", true));
            DrawMenu.Add("drawW", new CheckBox("Draw W Range", true));
            DrawMenu.Add("drawE", new CheckBox("Draw E Range", true));
            DrawMenu.Add("drawR", new CheckBox("Draw R Range", true));

            ComboMenu = Menu.AddSubMenu("Combo - " + GCharname, GCharname + "Combo");
            ComboMenu.AddGroupLabel("Combo");
            ComboMenu.Add("comboQ", new CheckBox("Use Q", true));
            ComboMenu.Add("comboW", new CheckBox("Use W", true));
            ComboMenu.Add("comboE", new CheckBox("Use E", true));
            ComboMenu.Add("comboR", new CheckBox("Use R", true));

            HarassMenu = Menu.AddSubMenu("Harass - " + GCharname, GCharname + "Harass");
            HarassMenu.AddGroupLabel("Harass");
            HarassMenu.Add("hsMana", new Slider("Minimum % Mana for Activate", 50, 1, 100));
            HarassMenu.AddGroupLabel("Q Settings");
            HarassMenu.Add("hsQ", new CheckBox("Use Q", true));
            HarassMenu.Add("hsQPassiveRange", new CheckBox("Only in passive range", true));
            HarassMenu.AddGroupLabel("W Settings");
            HarassMenu.Add("hsW", new CheckBox("Use W", true));
            HarassMenu.AddGroupLabel("E Settings");
            HarassMenu.Add("hsE", new CheckBox("Use E", true));

            LastHitMenu = Menu.AddSubMenu("Last Hit - " + GCharname, GCharname + "LastHit");
            LastHitMenu.AddGroupLabel("Last Hit");
            LastHitMenu.Add("lhMana", new Slider("Minimum % Mana for Activate", 50, 1, 100));
            LastHitMenu.AddGroupLabel("Q Settings");
            LastHitMenu.Add("lhQ", new CheckBox("Use Q", true));
            LastHitMenu.AddGroupLabel("E Settings");
            LastHitMenu.Add("lhE", new CheckBox("Use E", true));

            LaneClearMenu = Menu.AddSubMenu("Lane Clear - " + GCharname, GCharname + "LaneClear");
            LaneClearMenu.AddGroupLabel("Lane Clear");
            LaneClearMenu.Add("lcQ", new CheckBox("Use Q", true));
            LaneClearMenu.Add("lcW", new CheckBox("Use W", true));
            LaneClearMenu.Add("lcE", new CheckBox("Use E", true));
        }

        #region Events Region

        public override void OnDraw(EventArgs args)
        {
            if (Misc.IsChecked(DrawMenu, "drawDisable"))
                return;

            if (Misc.IsChecked(DrawMenu, "drawQ"))
                Circle.Draw(Q.IsReady() ? SharpDX.Color.Blue : SharpDX.Color.Red, Q.Range, Player.Instance.Position);

            if (Misc.IsChecked(DrawMenu, "drawW"))
                Circle.Draw(W.IsReady() ? SharpDX.Color.Blue : SharpDX.Color.Red, W.Range, Player.Instance.Position);

            if (Misc.IsChecked(DrawMenu, "drawE"))
                Circle.Draw(E.IsReady() ? SharpDX.Color.Blue : SharpDX.Color.Red, E.Range, Player.Instance.Position);

            if (Misc.IsChecked(DrawMenu, "drawR"))
                Circle.Draw(R.IsReady() ? SharpDX.Color.Blue : SharpDX.Color.Red, R.Range, Player.Instance.Position);
        }

        #endregion
    }
}
