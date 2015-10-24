using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using kTwitch2.Model;

namespace kTwitch2.Controller.Modes
{
    class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public override void Execute()
        {
            var t = AdvancedTargetSelector.GetTarget(RActive ? R.Range : W.Range, DamageType.Physical);
            if (t == null || !t.IsValidTarget()) return;

            if (W.IsReady())
            {
                var pred = W.GetPrediction(t);
                if (pred.HitChance >= HitChance.High)
                {
                    W.Cast(pred.CastPosition);
                }
            }

            if (E.IsReady())
            {
                var buffcount = t.GetBuffCount("twitchdeadlyvenom");
                if (buffcount >= 6)
                {
                    E.Cast();
                }
            }
        }
    }
}
