using System;
using BRSelector.Model;
using BRSelector.Model.Enum;
using BRSelector.Util;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;

namespace BRSelector
{
    public static class Selector
    {
        public static Menu menuTs,
            DrawMenu,
            SelectorMenu;

        public static External.Menu MenuExterno;

        internal static readonly string Version = "1.0.0";

        static Selector()
        {
            Init();    
        }

        static void Init()
        {
            MenuExterno = new External.Menu();

            //MenuExterno.ShowDialog();

            menuTs = MainMenu.AddMenu("BR Selector", "BrSelector");

            menuTs.AddLabel("Version: " + Version);
            menuTs.AddSeparator();
            menuTs.AddLabel("By KK2 and Vector");

            DrawMenu = menuTs.AddSubMenu("Draws", "Draw");
            var drawTarget = DrawMenu.Add("drawTarget", new CheckBox("Show target", true));
            drawTarget.OnValueChange += delegate
            {
                MenuExterno.showTarget.Checked = Misc.IsChecked(DrawMenu, "drawTarget");
            };

            var drawForcedTarget = DrawMenu.Add("drawForcedTarget", new CheckBox("Mark forced target", true));
            drawForcedTarget.OnValueChange += delegate
            {
                MenuExterno.drawForcedTarget.Checked = Misc.IsChecked(DrawMenu, "drawForcedTarget");
            };

            SelectorMenu = menuTs.AddSubMenu("Selector", "Selector");
            var forceTarget = SelectorMenu.Add("forceTarget", new CheckBox("Force Selected Target", true));
            forceTarget.OnValueChange += delegate
            {
                MenuExterno.forceSelectedTarget.Checked = Misc.IsChecked(DrawMenu, "forceTarget");
            };
            var sliderValue = SelectorMenu.Add("selectorType", new Slider("Selector Type", 0, 0, 8));
            sliderValue.OnValueChange += delegate
            {
                sliderValue.DisplayName = "Selector Type: " + Enum.GetName(typeof(EnumSelectorType), Misc.GetSliderValue(SelectorMenu, "selectorType"));
                MenuExterno.listBox1.SelectedIndex = Misc.GetSliderValue(SelectorMenu, "selectorType");
            };

            var counter = 1;
            foreach (var aiHeroClient in EntityManager.Heroes.Enemies)
            {
                var aux = SelectorMenu.Add("ts" + aiHeroClient.ChampionName, new Slider(aiHeroClient.ChampionName, AutoPriority.GetPriority(aiHeroClient.ChampionName), 1, 4));

                switch (counter)
                {
                    case 1:
                        MenuExterno.trackBar1.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar1.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                        break;
                    case 2:
                        MenuExterno.trackBar2.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar2.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                        break;
                    case 3:
                        MenuExterno.trackBar3.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar3.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                        break;
                    case 4:
                        MenuExterno.trackBar4.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar4.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                        break;
                    case 5:
                        MenuExterno.trackBar5.Text = aiHeroClient.ChampionName;
                        MenuExterno.trackBar5.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                        break;
                }

                aux.OnValueChange += delegate
                {
                    if (MenuExterno.trackBar1.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar1.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                    }
                    else if (MenuExterno.trackBar2.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar2.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                    }
                    else if (MenuExterno.trackBar3.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar3.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                    }
                    else if (MenuExterno.trackBar4.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar4.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                    }
                    else if (MenuExterno.trackBar5.Text == aiHeroClient.ChampionName)
                    {
                        MenuExterno.trackBar5.Value = Misc.GetSliderValue(SelectorMenu, "ts" + aiHeroClient.ChampionName);
                    }
                };
                counter++;
            }

        }
    }
}
