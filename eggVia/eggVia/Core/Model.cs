using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;

namespace eggVia.Core
{
    internal abstract class Model
    {
        /* Global vars */
        public static string G_version = "1.0.0.0";
        /* Spells */
        public static Spell.Skillshot Q;
        public static Spell.Skillshot W;
        public static Spell.Targeted E;
        public static Spell.Skillshot R;
        public static GameObject QMissle, RMissle;
        public static SpellSlot Ignite;
        /* Menu */

        public static Menu AniviaMenu;
        /* Misc */


        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
    }
}