using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using kTwitch2.Helpers;
using kTwitch2.Model;

namespace kTwitch2.Controller.Modes
{
    class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
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
                foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => e.IsValidTarget(E.Range) && e.IsEnemy &&
                                                                              e.IsVisible && !e.IsZombie && !e.IsDead &&
                                                                              e.HasBuff("twitchdeadlyvenom")))
                {
                    if (DmgLib.EDamage(enemy) >= enemy.Health)
                    {
                        E.Cast();
                    }
                }
            }

            ItemManager.UseYomu();
            ItemManager.UseBtrk(t);

            if (R.IsReady() && _Player.CountEnemiesInRange(R.Range) >= 1)
            {
                R.Cast();
            }

        }
    }
}
