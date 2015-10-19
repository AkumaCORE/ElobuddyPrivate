using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BRSelector.Helpers;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using SharpDX;

namespace BRSelector.Model
{
    class TargetSelector
    {
        internal static bool IsValidTarget(AIHeroClient target, 
            float range, 
            DamageType damageType,
            bool ignoreShield = true,
            Vector3 from = default(Vector3))
        {
            try
            {
                return target.IsValidTarget() &&
                       target.Distance(
                           (from.Equals(default(Vector3)) ? ObjectManager.Player.ServerPosition : from), true) <
                       Math.Pow((range <= 0 ? ObjectManager.Player.GetAutoAttackRange(target) : range), 2) &&
                       !Invulnerable.Check(target, damageType, ignoreShield);
            }
            catch (Exception ex)
            {
                Chat.Print(ex);
            }
            return false;
        }
    }
}
