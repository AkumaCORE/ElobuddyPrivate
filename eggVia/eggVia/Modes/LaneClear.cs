using System.Linq;
using eggVia.Core;
using eggVia.Utils;
using EloBuddy;
using EloBuddy.SDK;

namespace eggVia.Modes
{
    internal class LaneClear : Model
    {
        public static void useLC()
        {
            var minions =
                EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position, E.Range,
                    true).OrderByDescending(x => x.MaxHealth).FirstOrDefault();
            var m =
                EntityManager.MinionsAndMonsters.EnemyMinions.Where(
                    x => x.Distance(_Player.ServerPosition) <= R.Range);
            if (minions == null) return;
            if ((_Player.GetSpellDamage(minions, SpellSlot.E) >= minions.Health) ||
                (minions.HasBuff("Chilled") && _Player.GetSpellDamage(minions, SpellSlot.E) >= minions.Health*2) &&
                (!Orbwalker.IsAutoAttacking || !Orbwalker.CanAutoAttack))
            {
                E.Cast(minions);
            }
            if (R.IsReady() && RMissle == null && _Player.ManaPercent > 20)
            {
                if (m.Count() >= 3)
                {
                    var POS = Misc.GetBestCircularFarmLocation(m.Select(xm => xm.ServerPosition.To2D()).ToList(),
                        R.Width, R.Range);
                    R.Cast(POS.Position.To3D());
                }
            }
            if (R.IsReady() && RMissle != null)
            {
                var minR =
                    EntityManager.MinionsAndMonsters.EnemyMinions.Where(x => x.Distance(RMissle.Position) <= R.Width);
                if (minR.Count() < 2 || _Player.ManaPercent <= 20)
                {
                    //   R.Cast(RMissle.Position);
                    Player.CastSpell(SpellSlot.R, RMissle.Position);
                   // Player.CastSpell(SpellSlot.R, _Player.Position);
                }
            }
        }
    }
}