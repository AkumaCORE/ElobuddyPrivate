using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EloBuddy.SDK;
using kTwitch2.Helpers;
using kTwitch2.Model;

namespace kTwitch2.Controller.Modes
{
    class LaneClear : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.LaneClear);
        }

        public override void Execute()
        {
            var minions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, _Player.Position,
                W.Range);

            if (W.IsReady())
            {
                var wfarm = Misc.GetBestCircularFarmLocation(minions.Where(x => x.Distance(_Player) <= W.Range).Select(xm => xm.ServerPosition.To2D()).ToList(), W.Width, W.Range);
                if (wfarm.MinionsHit >= 3)
                {
                    W.Cast(wfarm.Position.To3D());
                }
            }
            if (E.IsReady())
            {
                var eminions = EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy,
                    _Player.Position, E.Range);
                var count = eminions.Count(m => DmgLib.EDamage(m) >= m.Health);
                if (count >= 2)
                    E.Cast();
            }
        }
    }
}
