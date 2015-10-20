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
                Console.WriteLine(ex);
            }
            return false;
        }
        
        public static AIHeroClient GetTarget(float range,
            DamageType damageType = DamageType.True,
            bool ignoreShields = true,
            Vector3 from = default(Vector3),
            IEnumerable<AIHeroClient> ignoredChampions = null)
        {
            try
            {
                var targets = GetTargets(range, damageType, ignoreShields, from, ignoredChampions);
                return targets != null ? targets.FirstOrDefault() : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        public static IEnumerable<AIHeroClient> GetTargets(float range,
            DamageType damageType = DamageType.True,
            bool ignoreShields = true,
            Vector3 from = default(Vector3),
            IEnumerable<AIHeroClient> ignoredChampions = null)
        {
            try
            {
                var mRange = Math.Max(range, 2000); // maximo de range q da pra chegar

                var selectedTarget = Selected.GetTarget(range, damageType, ignoreShields, from);
                if (selectedTarget != null)
                {
                    return new List<AIHeroClient> { selectedTarget };
                }

                var targets =
                    Humanizer.FilterTargets(Targets.Items)
                        .Where(
                            h => ignoredChampions == null || ignoredChampions.All(i => i.NetworkId != h.Hero.NetworkId))
                        .Where(h => IsValidTarget(h.Hero, range, damageType, ignoreShields, from))
                        .ToList();

                if (targets.Count > 0)
                {
                    var t = GetOrderedChampions(targets).ToList();
                    if (t.Count > 0)
                    {
                        if (Selected.Target != null && t.Count > 1)
                        {
                            t = t.OrderByDescending(x => x.Hero.NetworkId.Equals(Selected.Target.NetworkId)).ToList();
                        }
                        return t.Select(h => h.Hero).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new List<AIHeroClient>();
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
                    case 8: // Prioridade jovem? EOQ
                        return AutoPriority.OrderChampions(heroes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new List<Targets.Heroes>();
        } 
    }
}
