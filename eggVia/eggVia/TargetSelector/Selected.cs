using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Rendering;
using SharpDX;

namespace eggVia.TargetSelector
{
   internal class Selected
    {
        static Selected()
        {
            ClickBuffer = 100f;
            Game.OnWndProc += OnGameWndProc;
        }
        public static float ClickBuffer { get; set; }
        public static AIHeroClient Target { get; set; }

        public static AIHeroClient GetTarget(float range, DamageType damageType, bool ignoreShields, Vector3 from)
        {
            try
            {
                if (Target != null &&
                    TargetSelector.IsValidTarget(
                        Target, range, damageType, ignoreShields, from))
                {
                    return Target;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        private static void OnDrawingDraw(EventArgs args)
        {
            try
            {

                if (Target != null && Target.IsValidTarget() && Target.Position.IsOnScreen())
                {
                    Drawing.DrawCircle(Target.Position, 150, System.Drawing.Color.Red);
                   }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void OnGameWndProc(WndEventArgs args)
        {
            try
            {
                if (args.Msg != (ulong)WindowMessages.LeftButtonDown)
                {
                    return;
                }

                Target =
                    Targets.Items.Select(t => t.Hero)
                        .Where(h => h.IsValidTarget() && h.Distance(Game.CursorPos) < h.BoundingRadius + ClickBuffer)
                        .OrderBy(h => h.Distance(Game.CursorPos))
                        .FirstOrDefault();

                // quando tiver um menu ficar melhor nele neh parsa
                Drawing.OnDraw += OnDrawingDraw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
