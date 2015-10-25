using System.Collections.Generic;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace RealTimeInfo.Model.ServerModel
{
    class GameStatics
    {
        public float EnemyTeamGold { get; set; }
        public float AllyTeamGold { get; set; }
        public List<PlayerDamage> PlayersDamage = new List<PlayerDamage>();

        protected readonly List<AIHeroClient> Allies = EntityManager.Heroes.Allies;
        protected readonly List<AIHeroClient> Enemies = EntityManager.Heroes.Enemies;

        public GameStatics()
        {
            EnemyTeamGold = EnemyTeamGoldFunc();
            AllyTeamGold = AllyTeamGoldFunc();
            PlayersDamageListFill();
        }

        private void PlayersDamageListFill()
        {
            foreach (var hero in EntityManager.Heroes.AllHeroes)
            {
                PlayersDamage.Add(new PlayerDamage(hero.ChampionName,
                    hero.Level,
                    hero.GoldTotal,
                    hero.MagicDamageDealtPlayer + hero.PhysicalDamageDealtPlayer,
                    hero.PhysicalDamageDealtPlayer,
                    hero.MagicDamageDealtPlayer));
            }
        }

        private float AllyTeamGoldFunc()
        {
            return Allies.Sum(hero => hero.GoldTotal);
        }

        private float EnemyTeamGoldFunc()
        {
            return Enemies.Sum(hero => hero.GoldTotal);
        }
    }
}
