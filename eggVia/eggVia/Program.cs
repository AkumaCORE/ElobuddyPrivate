using System;
using eggVia.Core;
using EloBuddy;
using EloBuddy.SDK.Events;

namespace eggVia
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Loading.OnLoadingComplete += LoadingComplete;
        }

        private static void LoadingComplete(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName.ToLower() != "anivia") return;
            Anivia.Init();
        }
    }
}