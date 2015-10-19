using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRSelector.Model.ServerModels
{
    class GameStatics
    {
        public float enemyTeamGold { get; set; }
        public float allyTeamGold { get; set; }
        public List<PlayerDamage> PlayersDamage { get; set; }

        protected readonly List<AIHeroClient> allies = EntityManager.Heroes.Allies;
        protected readonly List<AIHeroClient> enemies = EntityManager.Heroes.Enemies;

        public GameStatics()
        {
            enemyTeamGold = EnemyTeamGoldFunc();
            allyTeamGold = AllyTeamGoldFunc();
            PlayersDamageListFill();
        }

        private void PlayersDamageListFill()
        {
            foreach (var hero in enemies)
            {
                PlayersDamage.Add(new PlayerDamage(hero.ChampionName, hero.TotalAttackDamage + hero.TotalMagicalDamage));
            }

            foreach (var hero in allies)
            {
                PlayersDamage.Add(new PlayerDamage(hero.ChampionName, hero.TotalAttackDamage + hero.TotalMagicalDamage));
            }
        }

        private float AllyTeamGoldFunc()
        {
            var gold = 0f;

            foreach (var hero in allies)
            {
                gold += hero.GoldTotal;
            }

            return gold;
        }

        private float EnemyTeamGoldFunc()
        {
            var gold = 0f;

            foreach (var hero in enemies)
            {
                gold += hero.GoldTotal;
            }

            return gold;
        }
    }
}
