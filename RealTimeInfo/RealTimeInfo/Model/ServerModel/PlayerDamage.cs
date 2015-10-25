namespace RealTimeInfo.Model.ServerModel
{
    class PlayerDamage
    {
        public string ChampionName { get; set; }
        public int ChampionLevel { get; set; }
        public float GoldEarned { get; set; }
        public float TotalDamageDealt { get; set; }
        public float PhysicalDamageDealt { get; set; }
        public float MagicallDamageDealt { get; set; }

        public PlayerDamage(string championName, int championLevel, float goldEarned, float totalDamageDealt, float physicalDamageDealt, float magicallDamageDealt)
        {
            ChampionLevel = championLevel;
            ChampionName = championName;
            GoldEarned = goldEarned;
            TotalDamageDealt = totalDamageDealt;
            PhysicalDamageDealt = physicalDamageDealt;
            MagicallDamageDealt = magicallDamageDealt;
        }
    }
}
