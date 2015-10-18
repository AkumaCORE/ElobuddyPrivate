using System.Linq;
using eggVia.Core;
using EloBuddy;
using EloBuddy.SDK;

namespace eggVia.Modes
{
    internal class LastHit : Model
    {
        public static void useLH()
        {
            var m = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position,
                E.Range).OrderByDescending(x => x.MaxHealth).FirstOrDefault();
            if (m == null || Orbwalker.IsAutoAttacking || Orbwalker.CanAutoAttack) return;
            if (_Player.GetSpellDamage(m, SpellSlot.E) >= m.Health)
            {
                E.Cast(m);
            }
        }
    }
}