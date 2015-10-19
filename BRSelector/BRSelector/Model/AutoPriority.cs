using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;

namespace BRSelector.Model
{
    class AutoPriority
    {
        static AutoPriority()
        {
            try
            {
                HPrio = new HashSet<Heroes>
                {
                    new Heroes()
                    {
                        Champions = new[]
                        {
                            "Ahri", "Anivia", "Annie", "Ashe", "Azir", "Brand", "Caitlyn", "Cassiopeia", "Corki",
                            "Draven", "Ezreal", "Graves", "Jinx", "Kalista", "Karma", "Karthus", "Katarina",
                            "Kennen", "KogMaw", "Leblanc", "Lucian", "Lux", "Malzahar", "MasterYi", "MissFortune",
                            "Orianna", "Quinn", "Sivir", "Syndra", "Talon", "Teemo", "Tristana", "TwistedFate",
                            "Twitch", "Varus", "Vayne", "Veigar", "VelKoz", "Viktor", "Xerath", "Zed", "Ziggs"
                        },
                        Danger = 1
                    },
                    new Heroes()
                    {
                        Champions = new[]
                        {
                             "Akali", "Diana", "Ekko", "Fiddlesticks", "Fiora", "Fizz", "Heimerdinger", "Jayce",
                                "Kassadin", "Kayle", "Kha'Zix", "Lissandra", "Mordekaiser", "Nidalee", "Riven", "Shaco",
                                "Vladimir", "Yasuo", "Zilean", "Kindred"
                        },
                        Danger = 2
                    },
                    new Heroes()
                    {
                        Champions = new[]
                        {
                             "Aatrox", "Darius", "Elise", "Evelynn", "Galio", "Gangplank", "Gragas", "Irelia", "Jax",
                                "Lee Sin", "Maokai", "Morgana", "Nocturne", "Pantheon", "Poppy", "Rengar", "Rumble",
                                "Ryze", "Swain", "Trundle", "Tryndamere", "Udyr", "Urgot", "Vi", "XinZhao", "RekSai"
                        },
                        Danger = 3
                    },
                    new Heroes()
                    {
                        Champions = new[]
                        {
                             "Alistar", "Amumu", "Bard", "Blitzcrank", "Braum", "Cho'Gath", "Dr. Mundo", "Garen",
                                "Gnar", "Hecarim", "Janna", "Jarvan IV", "Leona", "Lulu", "Malphite", "Nami", "Nasus",
                                "Nautilus", "Nunu", "Olaf", "Rammus", "Renekton", "Sejuani", "Shen", "Shyvana", "Singed",
                                "Sion", "Skarner", "Sona", "Soraka", "Taric", "Thresh", "Volibear", "Warwick",
                                "MonkeyKing", "Yorick", "Zac", "Zyra", "Tahm Kench"
                        },
                        Danger = 4
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public static int GetPriority(string name)
        {
            try
            {
                var item = HPrio.FirstOrDefault(i => i.Champions.Contains(name));
                if (item != null)
                {
                    return item.Danger;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return 4; // baixo se nada né lek
        }

        public static IEnumerable<Targets.Heroes> OrderChampions(List<Targets.Heroes> heroes)
        {
            try
            {
                return heroes.OrderByDescending(x => GetPriority(x.Hero.ChampionName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new List<Targets.Heroes>();
        }

        public static IEnumerable<Targets.Heroes> OrderChampionsWithHealh(List<Targets.Heroes> heroes)
        {
            try
            {
                return heroes.OrderByDescending(x => GetPriority(x.Hero.ChampionName)).ThenBy(x => x.Hero.Health); ;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return new List<Targets.Heroes>();
        }

        public static HashSet<Heroes> HPrio { get; private set; }

        public class Heroes
        {
            public string[] Champions { get; set; }
            public int Danger { get; set; }
        }
    }
}
