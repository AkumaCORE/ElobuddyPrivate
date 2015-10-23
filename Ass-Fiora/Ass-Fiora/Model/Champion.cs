using System;
using System.Drawing;
using Ass_Fiora.Controller;
using Ass_Fiora.Model.Enum;
using Ass_Fiora.Model.Languages;
using BRSelector;
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

            #region Language Selector

            MiscMenu = Menu.AddSubMenu("Misc - " + GCharname, GCharname + "Misc");
            var sliderValue = MiscMenu.Add("language", new Slider("Language", 0, 0, 4));
            sliderValue.OnValueChange += delegate
            {
                sliderValue.DisplayName = "Language: " + System.Enum.GetName(typeof(EnumLanguage), Misc.GetSliderValue(MiscMenu, "language"));
            };
            MiscMenu.AddLabel("After select your language press F5");

            LanguageController language;

            switch ((EnumLanguage)Misc.GetSliderValue(MiscMenu, "language"))
            {
                case EnumLanguage.English:
                    language = new English();
                    break;
                case EnumLanguage.Portugues:
                    language = new Portugues();
                    break;
                case EnumLanguage.Deutsch:
                    language = new Deutsch();
                    break;
                case EnumLanguage.Espanol:
                    language = new Espanol();
                    break;
                case EnumLanguage.Francais:
                    language = new Francais();
                    break;
                default:
                    language = new English();
                    break;
            }

            #endregion

            DrawMenu = Menu.AddSubMenu(language.Dictionary[EnumContext.Draw] + " - " + GCharname, GCharname + "Draw");
            DrawMenu.AddGroupLabel(language.Dictionary[EnumContext.Draw]);
            DrawMenu.Add("drawDisable", new CheckBox(language.Dictionary[EnumContext.TurnOffDraws], true));
            DrawMenu.Add("drawQ", new CheckBox(language.Dictionary[EnumContext.Draw] + language.Dictionary[EnumContext.Range] + " Q", true));
            DrawMenu.Add("drawW", new CheckBox(language.Dictionary[EnumContext.Draw] + language.Dictionary[EnumContext.Range] + " W", true));
            DrawMenu.Add("drawE", new CheckBox(language.Dictionary[EnumContext.Draw] + language.Dictionary[EnumContext.Range] + " E", true));
            DrawMenu.Add("drawR", new CheckBox(language.Dictionary[EnumContext.Draw] + language.Dictionary[EnumContext.Range] + " R", true));

            ComboMenu = Menu.AddSubMenu(language.Dictionary[EnumContext.Combo] + " - " + GCharname, GCharname + "Combo");
            ComboMenu.AddGroupLabel(language.Dictionary[EnumContext.Combo]);
            ComboMenu.Add("comboQ", new CheckBox(language.Dictionary[EnumContext.Use] + " Q", true));
            ComboMenu.Add("comboQPassiveRange", new CheckBox(language.Dictionary[EnumContext.OnlyInPassiverange], true));
            ComboMenu.Add("comboW", new CheckBox(language.Dictionary[EnumContext.Use] + " W", true));
            ComboMenu.Add("comboE", new CheckBox(language.Dictionary[EnumContext.Use] + " E", true));
            ComboMenu.Add("comboR", new CheckBox(language.Dictionary[EnumContext.Use] + " R", true));

            HarassMenu = Menu.AddSubMenu(language.Dictionary[EnumContext.Harass] + " - " + GCharname, GCharname + "Harass");
            HarassMenu.AddGroupLabel(language.Dictionary[EnumContext.Harass]);
            HarassMenu.Add("hsMana", new Slider(language.Dictionary[EnumContext.MinimunMana], 50, 1, 100));
            HarassMenu.AddGroupLabel("Q " + language.Dictionary[EnumContext.Settings]);
            HarassMenu.Add("hsQ", new CheckBox(language.Dictionary[EnumContext.Use] + " Q", true));
            HarassMenu.Add("hsQPassiveRange", new CheckBox(language.Dictionary[EnumContext.OnlyInPassiverange], true));
            HarassMenu.AddGroupLabel("W " + language.Dictionary[EnumContext.Settings]);
            HarassMenu.Add("hsW", new CheckBox(language.Dictionary[EnumContext.Use] + " W", true));
            HarassMenu.AddGroupLabel("E " + language.Dictionary[EnumContext.Settings]);
            HarassMenu.Add("hsE", new CheckBox(language.Dictionary[EnumContext.Use] + " E", true));

            LastHitMenu = Menu.AddSubMenu(language.Dictionary[EnumContext.LastHit] + " - " + GCharname, GCharname + "LastHit");
            LastHitMenu.AddGroupLabel(language.Dictionary[EnumContext.LastHit]);
            LastHitMenu.Add("lhMana", new Slider(language.Dictionary[EnumContext.MinimunMana], 50, 1, 100));
            LastHitMenu.AddGroupLabel("Q " + language.Dictionary[EnumContext.Settings]);
            LastHitMenu.Add("lhQ", new CheckBox(language.Dictionary[EnumContext.Use] + " Q", true));
            LastHitMenu.AddGroupLabel("E " + language.Dictionary[EnumContext.Settings]);
            LastHitMenu.Add("lhE", new CheckBox(language.Dictionary[EnumContext.Use] + " E", true));

            LaneClearMenu = Menu.AddSubMenu(language.Dictionary[EnumContext.LaneClear] + " - " + GCharname, GCharname + "LaneClear");
            LaneClearMenu.AddGroupLabel(language.Dictionary[EnumContext.LaneClear]);
            LaneClearMenu.Add("lcQ", new CheckBox(language.Dictionary[EnumContext.Use] + " Q", true));
            LaneClearMenu.Add("lcW", new CheckBox(language.Dictionary[EnumContext.Use] + " W", true));
            LaneClearMenu.Add("lcE", new CheckBox(language.Dictionary[EnumContext.Use] + " E", true));
        }

        #region Events Region

        public override void OnAfterAttack(AttackableUnit target, EventArgs args)
        {
            if(ActiveMode == EnumModeManager.None) return;

            if (E.IsReady() && ((Misc.IsChecked(ComboMenu, "comboE") && ActiveMode == EnumModeManager.Combo) 
                || (Misc.IsChecked(HarassMenu, "hsE") && ActiveMode == EnumModeManager.Harass) 
                || (Misc.IsChecked(LaneClearMenu, "lcE") && ActiveMode == EnumModeManager.LaneClear)))
            {
                E.Cast();
            }
        }

        public override void OnProcessSpell(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            RiposteManager.CheckDangerousSpellsIncoming(sender, args);
        }

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
