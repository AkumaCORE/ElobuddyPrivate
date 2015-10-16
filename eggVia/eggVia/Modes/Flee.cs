using eggVia.Core;
using EloBuddy;
using EloBuddy.SDK;

namespace eggVia.Modes
{
    internal class Flee : Model
    {
        public static void useFee()
        {
            var t = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            if (t == null) return;
            if (W.IsReady())
            {
                W.Cast(t.Position.Extend(_Player.Position, -50).To3D()); // IN YOUR FACE!
            }
        }
    }
}