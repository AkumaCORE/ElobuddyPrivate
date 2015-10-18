using System;
using eggVia.Core;
using EloBuddy;

namespace eggVia.Utils
{
    internal class LevelUP : Model
    {
        private static readonly int[] abilitySequence = {1, 3, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2};
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;

        public static void Game_OnUpdate(EventArgs args)
        {
            var qL = _Player.Spellbook.GetSpell(SpellSlot.Q).Level + qOff;
            var wL = _Player.Spellbook.GetSpell(SpellSlot.W).Level + wOff;
            var eL = _Player.Spellbook.GetSpell(SpellSlot.E).Level + eOff;
            var rL = _Player.Spellbook.GetSpell(SpellSlot.R).Level + rOff;
            if (qL + wL + eL + rL < ObjectManager.Player.Level)
            {
                int[] level = {0, 0, 0, 0};
                for (var i = 0; i < ObjectManager.Player.Level; i++)
                {
                    level[abilitySequence[i] - 1] = level[abilitySequence[i] - 1] + 1;
                }
                if (qL < level[0]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.Q);
                if (wL < level[1]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.W);
                if (eL < level[2]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.E);
                if (rL < level[3]) ObjectManager.Player.Spellbook.LevelSpell(SpellSlot.R);
            }
        }
    }
}