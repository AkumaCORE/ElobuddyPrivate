using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using Veigar.Helpers;
using Veigar.Model;

namespace Veigar.Controller.Modes
{
    internal class Combo : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var t = AdvancedTargetSelector.GetTarget(Q.Range, DamageType.Magical);

            if (t == null || !t.IsValidTarget()) return;

            Orbwalker.ForcedTarget = t;

            if (E.IsReady())
            {
                var pred = E.GetPrediction(t);
                var unit = pred.UnitPosition;
                var okay = _Player.Distance(unit) <=
                           E.Range + Misc.GetArrivalTime(_Player.Distance(unit), E.CastDelay, E.Speed)*t.MoveSpeed;
                if (okay)
                    //E.Cast(t.Position.Shorten(_Player.Position, 150));
                    E.Cast(pred.CastPosition);
            }

            if (Q.IsReady())
            {
                var pred = Q.GetPrediction(t);
                if (pred.HitChancePercent >= 70)
                    Q.Cast(pred.CastPosition);
            }

            if (W.IsReady())
            {
                var pred = W.GetPrediction(t);
                if (pred.HitChance >= HitChance.Immobile)
                    W.Cast(pred.CastPosition);
                else if (pred.HitChance >= HitChance.High)
                    W.Cast(pred.CastPosition);
            }



            if (R.IsReady())
            {
                if (DamageLib.RDamage(t) >= t.Health)
                {
                    Chat.Print("tentei castar R");
                    Player.CastSpell(SpellSlot.R, t);
                }
            }

            if (_Player.GetSummonerSpellDamage(t, DamageLibrary.SummonerSpells.Ignite) >= t.Health &&
                        _Player.Distance(t) <= 0x258)
            {
                _Player.Spellbook.CastSpell(Ignite, t);
            }
        }
    }
}
