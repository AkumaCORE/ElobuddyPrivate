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
