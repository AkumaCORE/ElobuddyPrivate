using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eggVia.Core;
using EloBuddy;

namespace eggVia.Utils
{
    internal class LevelUP : Model
    {
        private static int[] abilitySequence = {1, 3, 3, 2, 3, 4, 3, 1, 3, 1, 4, 1, 1, 2, 2, 4, 2, 2};
        public static int qOff = 0, wOff = 0, eOff = 0, rOff = 0;

        public static void Game_OnUpdate(EventArgs args)
        {
            int qL = _Player.Spellbook.GetSpell(SpellSlot.Q).Level + qOff;
            int wL = _Player.Spellbook.GetSpell(SpellSlot.W).Level + wOff;
            int eL = _Player.Spellbook.GetSpell(SpellSlot.E).Level + eOff;
            int rL = _Player.Spellbook.GetSpell(SpellSlot.R).Level + rOff;
            if (qL + wL + eL + rL < ObjectManager.Player.Level)
            {
                int[] level = new int[] {0, 0, 0, 0};
                for (int i = 0; i < ObjectManager.Player.Level; i++)
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
