﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace Veigar.Model
{
    public static class MenuX
    {
        private const string MenuName = "Veigar";
        private static readonly Menu Menu;
        private static readonly string[] _modeEstring = { "Place on Mid", "Place on Border" };

        static MenuX()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("BigBoSS ReW0rK3d");
            Menu.AddSeparator();
            Menu.AddLabel("Made by Kk2 (:");
            Modes.Initialize();
        }
        public static void Initialize() { }

        public static class Modes
        {
            private static readonly Menu Menu;

            static Modes()
            {
                Menu = MenuX.Menu.AddSubMenu("Modes");
                Combo.Initilize();
                Harass.Initialize();
                LaneClear.Initialize();
                LastHit.Initialize();
                Drawings.Initialize();
                Misc.Initialize();
            }

            public static void Initialize()
            {
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _modeE;
                private static readonly CheckBox _useR;
                private static readonly CheckBox _useIgnite;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }

                public static int ModeE
                {
                    get { return _modeE.CurrentValue; }
                }

                public static bool UseIg
                {
                    get { return _useIgnite.CurrentValue; }
                }

                static Combo()
                {
                    Menu.AddGroupLabel("Combo");
                    _useQ = Menu.Add("useQCombo", new CheckBox("Use Q"));
                    _useW = Menu.Add("useWCombo", new CheckBox("Use W"));
                    _useE = Menu.Add("useECombo", new CheckBox("Use E"));
                    _modeE = Menu.Add("ModeE", new Slider("E Mode: ", 0, 0, 1));
                    _modeE.OnValueChange += delegate
                    {
                        _modeE.DisplayName = "E Mode: " + _modeEstring[_modeE.CurrentValue];
                    };
                    _modeE.DisplayName = "E Mode: " + _modeEstring[_modeE.CurrentValue];
                    _useR = Menu.Add("useRCombo", new CheckBox("Use R"));
                    _useIgnite = Menu.Add("useIgCombo", new CheckBox("Use Ignite to Kill"));
                    Menu.AddSeparator();
                }

                public static void Initilize()
                {
                }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _useE;
                private static readonly Slider _modeE;
                private static readonly Slider _hMana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static bool UseE
                {
                    get { return _useE.CurrentValue; }
                }

                public static int ModeE
                {
                    get { return _modeE.CurrentValue; }
                }

                public static int MinMana
                {
                    get { return _hMana.CurrentValue; }
                }

                static Harass()
                {
                    Menu.AddGroupLabel("Harass");
                    _useQ = Menu.Add("useQHarass", new CheckBox("Use Q"));
                    _useW = Menu.Add("useWHarass", new CheckBox("Use W"));
                    _useE = Menu.Add("useEHarass", new CheckBox("Use E"));
                    _modeE = Menu.Add("modeEHarass", new Slider("E Mode: ", 0, 0, 1));
                    _modeE.OnValueChange += delegate
                    {
                        _modeE.DisplayName = "E Mode: " + _modeEstring[_modeE.CurrentValue];
                    };
                    _modeE.DisplayName = "E Mode: " + _modeEstring[_modeE.CurrentValue];
                    _hMana = Menu.Add("ManaHarass", new Slider("Min Mana %> to Harass", 20));
                    Menu.AddSeparator();

                }

                public static void Initialize()
                {
                }
            }

            public static class LaneClear
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _minW;
                private static readonly Slider _lMana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }

                public static int MinMinion
                {
                    get { return _minW.CurrentValue; }
                }

                public static int MinMana
                {
                    get { return _lMana.CurrentValue; }
                }

                static LaneClear()
                {
                    Menu.AddGroupLabel("LaneClear");
                    _useQ = Menu.Add("useQLane", new CheckBox("Use Q"));
                    _useW = Menu.Add("useWLane", new CheckBox("Use W"));
                    _minW = Menu.Add("minWL", new Slider("Min Minions to use W", 2, 0, 5));
                    _lMana = Menu.Add("ManaLane", new Slider("Min Mana %> to LaneClear", 20));
                    Menu.AddSeparator();
                }

                public static void Initialize()
                {
                }
            }

            public static class LastHit
            {
                private static readonly CheckBox _useQ;
                private static readonly Slider _ltMana;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }

                public static int MinMana
                {
                    get { return _ltMana.CurrentValue; }
                }

                static LastHit()
                {
                    Menu.AddGroupLabel("LastHit");
                    _useQ = Menu.Add("UseQLast", new CheckBox("Use Q"));
                    _ltMana = Menu.Add("ManaFarm", new Slider("Min Mana %> to LastHit", 40));
                    Menu.AddSeparator();
                }

                public static void Initialize()
                {
                }
            }
        }

        public static class AutoQ
        {
            public const string MenuName = "AutoQ";
            private static readonly Menu Menu;
            private static readonly CheckBox _useQ;
            private static readonly KeyBind _bindQ;
            private static readonly Slider _autoMana;

            public static bool UseQ
            {
                get { return _useQ.CurrentValue; }
            }
            public static int MinMana
            {
                get { return _autoMana.CurrentValue; }
            }
            public static bool BindQ
            {
                get { return _bindQ.CurrentValue; }
            }
            static AutoQ()
            {
                Menu = Menu.AddSubMenu(MenuName);
                Menu.AddGroupLabel("Auto Q Farm");
                _useQ = Menu.Add("useQAuto", new CheckBox("Use Auto Q"));
                _autoMana = Menu.Add("minAutoMana", new Slider("Min %> Mana to AutoQ", 40));
                _bindQ = Menu.Add("keyQ", new KeyBind("Auto Q Key Farm (Toogle)", false, KeyBind.BindTypes.PressToggle, 'N'));
            }
        }

        public static class Drawings
        {
            public const string MenuName = "Drawings";
            private static readonly Menu Menu;
            private static readonly CheckBox _drawQ;
            private static readonly CheckBox _drawW;
            private static readonly CheckBox _drawE;
            private static readonly CheckBox _drawR;

            public static bool DrawQ
            {
                get { return _drawQ.CurrentValue; }
            }
            public static bool DrawW
            {
                get { return _drawW.CurrentValue; }
            }
            public static bool DrawE
            {
                get { return _drawE.CurrentValue; }
            }
            public static bool DrawR
            {
                get { return _drawR.CurrentValue; }
            }

            static Drawings()
            {
                Menu = MenuX.Menu.AddSubMenu(MenuName);
                Menu.AddGroupLabel("Spell Ranges");
                _drawQ = Menu.Add("drawQ", new CheckBox("Q Range"));
                _drawW = Menu.Add("drawW", new CheckBox("W Range"));
                _drawE = Menu.Add("drawE", new CheckBox("E Range"));
                _drawR = Menu.Add("drawR", new CheckBox("R Range"));
            }

            public static void Initialize()
            {
                
            }
        }

        public static class Misc
        {
            public const string MenuName = "Misc";
            private static readonly Menu Menu;
            private static readonly Slider _skinHax;
            static Misc()
            {
                Menu = MenuX.Menu.AddSubMenu(MenuName);
                Menu.AddGroupLabel("Misc Options");
                _skinHax = Menu.Add("skinX", new Slider("Choose your Skin [Number]", 8, 0, 8));
                _skinHax.OnValueChange += delegate
                {
                    ObjectManager.Player.SetSkinId(_skinHax.CurrentValue);
                };
                ObjectManager.Player.SetSkinId(_skinHax.CurrentValue);

            }
            public static void Initialize() { }
        }


    }
}
