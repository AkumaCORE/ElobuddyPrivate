namespace RealTimeInfo.Model.ServerModel
{
    class PlayerDamage
    {
        public string ChampionName { get; set; }
        public float DamageDealt { get; set; }

        public PlayerDamage(string championName, float damageDealt)
        {
            ChampionName = championName;
            DamageDealt = damageDealt;
        }
    }
}
