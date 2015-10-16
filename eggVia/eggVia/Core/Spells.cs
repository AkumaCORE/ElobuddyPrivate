using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;

namespace eggVia.Core
{
    internal class Spells : Model
    {
        public static void setSpells()
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 1250, SkillShotType.Linear, (int) 0.25f, 870, 110)
            {
                AllowedCollisionCount = int.MaxValue
            };
            W = new Spell.Skillshot(SpellSlot.W, 950, SkillShotType.Linear, (int) 0.6f, int.MaxValue, 1);
            E = new Spell.Targeted(SpellSlot.E, 650);
            R = new Spell.Skillshot(SpellSlot.R, 650, SkillShotType.Circular, 2, int.MaxValue, 400);
            Ignite = ObjectManager.Player.GetSpellSlotFromName("summonerdot");
        }
    }
}