using System.Collections.Generic;
using System.Linq;
using Ass_Fiora.Helpers;
using Ass_Fiora.Model;
using Ass_Fiora.Model.Enum;
using BRSelector.Model;
using EloBuddy;
using EloBuddy.SDK;
using OneForWeek.Util.Misc;
using SharpDX;

namespace Ass_Fiora.Controller.Modes
{
    public sealed class Combo : ModeBase
    {

        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            var q = PluginModel.Q;
            var w = PluginModel.W;
            var e = PluginModel.E;
            var r = PluginModel.R;

            var target = AdvancedTargetSelector.GetTarget(q.Range, DamageType.Physical);

            if (target == null || !target.IsValidTarget()) return;

            PluginModel.ActiveMode = EnumModeManager.Combo;

            if (q.IsReady() && Misc.IsChecked(PluginModel.ComboMenu, "comboQ"))
            {
                var targetpos = Prediction.Position.PredictUnitPosition(target, 250);

                if (Misc.IsChecked(PluginModel.ComboMenu, "comboQPassiveRange") && q.IsInRange(targetpos.To3D()) &&
                    PassiveController.HasPassive(target))
                {
                    var poses = PassiveController.PassiveRadiusPoint(target);
                    var pos = target.Position.To2D().Extend(PassiveController.PassivePosition(target).To2D(), 100);
                    var possibleposes = new List<Vector2>();

                    for (var i = 0; i <= 400; i = i + 100)
                    {
                        var p = Player.Instance.Position.To2D().Extend(pos, i);
                        possibleposes.Add(p);
                    }

                    var castpos =
                        possibleposes.Where(x => x.To3D().InTheCone(poses, targetpos) && x.Distance(targetpos) <= 300)
                            .OrderByDescending(x => 1 - x.Distance(target.Position.To2D()))
                            .FirstOrDefault();

                    if (castpos.IsValid() && castpos.Distance(targetpos) <= 300)
                    {
                        Player.CastSpell(SpellSlot.Q, castpos.To3D());
                    }
                }
                else
                {
                    Player.CastSpell(SpellSlot.Q, Game.CursorPos);
                }
            }

            if (w.IsReady() && Misc.IsChecked(PluginModel.ComboMenu, "comboW"))
            {
                if (q.IsReady() || !(target.Distance(Player.Instance) > Player.Instance.GetAutoAttackRange())) return;

                var prediction = w.GetPrediction(target);

                if (prediction.HitChancePercent >= 70)
                {
                    w.Cast(prediction.CastPosition);
                }
            }
        }
    }
}
