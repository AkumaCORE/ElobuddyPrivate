using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eggVia.Core;
using EloBuddy;
using EloBuddy.SDK;

namespace eggVia.Modes
{
    class LastHit : Model
    {
        public static void useLH()
        {
            var m = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position,
                E.Range).OrderByDescending(x => x.MaxHealth).FirstOrDefault();
            if (m == null) return;
            if (_Player.GetSpellDamage(m, SpellSlot.E) >= m.Health && !Orbwalker.IsAutoAttacking)
            {
                E.Cast(m);
            }
        }
    }
}
