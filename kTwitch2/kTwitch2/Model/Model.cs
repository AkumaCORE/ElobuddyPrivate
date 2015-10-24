using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace kTwitch2.Model
{
    public abstract class Model
    {
        /* 
        Spells
        */
        public static Spell.Active Q { get; set; }
        public static Spell.Skillshot W { get; set; }
        public static Spell.Active E { get; set; }
        public static Spell.Active R { get; set; }

        /*
        Items
        */
        public static Item BTRK { get; set; }
        public static Item CutL { get; set; }
        public static Item Youmu { get; set; }
        public static Item Potion { get; set; }

        /*
        Utils
        */
        public static AIHeroClient _Player { get { return ObjectManager.Player; } }

        public static bool RActive
        {
            get { return _Player.HasBuff("TwitchFullAutomatic"); }
        }
    }
}
