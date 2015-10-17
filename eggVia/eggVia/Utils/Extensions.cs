using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;

namespace eggVia.Utils
{
    public static class Extensions
    {
        internal static bool HasBuffUntil(
           this AIHeroClient unit,
           string displayName,
           float tickCount,
           bool includePing = true)
        {
            try
            {
                return
                    unit.Buffs.Any(
                        buff =>
                        buff.IsValid
                        && string.Equals(buff.DisplayName, displayName, StringComparison.CurrentCultureIgnoreCase)
                        && buff.EndTime - Game.Time > tickCount - (includePing ? (Game.Ping / 2000f) : 0));
            }
            catch (Exception e)
            {
                Console.WriteLine(" " + e);
                throw;
            }
        }
        public static bool IsFacing2(this Obj_AI_Base source, Obj_AI_Base target)
        {
            return (source.IsValid() && target.IsValid())
                   && source.Direction.AngleBetween(target.Position - source.Position) < 90;
        }
    }
}
