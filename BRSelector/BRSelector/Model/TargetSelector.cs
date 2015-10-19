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
        public static int Mode { get; set; }
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

        private static IEnumerable<Targets.Heroes> GetOrderedChampions(List<Targets.Heroes> heroes)
        {
            try
            {
                switch (Mode)
                {
                    case 0: // Auto Priority
                        return AutoPriority.OrderChampionsWithHealh(heroes);
                    case 1: // less attack?
                        return heroes.OrderBy(x => x.Hero.Health/ObjectManager.Player.TotalAttackDamage);
                    case 2: // MOST AP?
                        return heroes.OrderBy(x => x.Hero.TotalMagicalDamage);
                    case 3: // MOST AD ?
                        return heroes.OrderBy(x => x.Hero.TotalAttackDamage);
                    case 4: // PERTIN
                        return heroes.OrderBy(x => x.Hero.Distance(ObjectManager.Player));
                    case 5: // PERTO DO MAUSEÇ 
                        return heroes.OrderBy(x => x.Hero.Distance(Game.CursorPos));
                    case 6: // LESS CASTERINO?
                        return heroes.OrderBy(x => x.Hero.Health/ObjectManager.Player.TotalMagicalDamage);
                    case 7: // MAIS SEM VIDA
                        return heroes.OrderBy(x => x.Hero.Health);
                    case 8:
                        return AutoPriority.OrderChampions(heroes);
                }
            }
            catch (Exception ex)
            {
                Chat.Print(ex);
            }
            return new List<Targets.Heroes>();
        } 
    }
}
