using System;
using System.Linq;
using EloBuddy.SDK;
using kTwitch2.Helpers;
using kTwitch2.Model;

namespace kTwitch2.Controller.Modes
{
    internal class JungleClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.JungleClear);
        }

        public override void Execute()
        {
            var m =
                EntityManager.MinionsAndMonsters.GetJungleMonsters(_Player.Position, W.Range)
                    .OrderByDescending(x => x.Health)
                    .ToList();
            if (!m.Any()) return;

            if (W.IsReady())
            {
                W.Cast(m.First().ServerPosition);
            }
            if (E.IsReady())
            {
                var mk = m.Where(x => x.Health + (x.PercentHealingAmountMod/2) < DmgLib.EDamage(x));
                if (mk.Any())
                {
                    E.Cast();
                }
            }
        }
    }
}
