using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRSelector.Model.ServerModels
{
    class PlayerDamage
    {
        public string championName { get; set; }
        public float damageDealth { get; set; }

        public PlayerDamage(string championName, float damageDealth)
        {
            this.championName = championName;
            this.damageDealth = damageDealth;
        }
    }
}
