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

        internal static readonly string version = "1.0.0";

        static Selector()
        {
            Init();    
        }

        static void Init()
        {
            menuTs = MainMenu.AddMenu("BR Selector", "BrSelector");

            menuTs.AddLabel("Version: " + version);
            menuTs.AddSeparator();
            menuTs.AddLabel("By KK2 and Vector");

            DrawMenu = menuTs.AddSubMenu("Draws", "Draw");
            DrawMenu.Add("drawTarget", new CheckBox("Show target", true));
            DrawMenu.Add("drawForcedTarget", new CheckBox("Mark forced target", true));

            SelectorMenu = menuTs.AddSubMenu("Selector", "Selector");
            SelectorMenu.Add("forceTarget", new CheckBox("Force Selected Target", true));
            var sliderValue = SelectorMenu.Add("selectorType", new Slider("Selector Type", 0, 0, 8));
            sliderValue.OnValueChange += delegate
            {
                sliderValue.DisplayName = "Selector Type: " + Enum.GetName(typeof(EnumSelectorType), Misc.GetSliderValue(SelectorMenu, "selectorType"));
            };

            foreach (var aiHeroClient in EntityManager.Heroes.Enemies)
            {
                SelectorMenu.Add("ts" + aiHeroClient.ChampionName, new Slider(aiHeroClient.ChampionName, AutoPriority.GetPriority(aiHeroClient.ChampionName), 1, 4));
            }

        }
    }
}
