using Ass_Fiora.Model;
using EloBuddy.SDK;

namespace Ass_Fiora.Controller
{
    public sealed class Combo : ModeBase
    {

        public override bool ShouldBeExecuted()
        {
            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo);
        }

        public override void Execute()
        {
            PluginModel.ActiveMode = EnumModeManager.Combo;

            var q = PluginModel.Q;
            var w = PluginModel.W;
            var e = PluginModel.E;
            var r = PluginModel.R;


        }
    }
}
