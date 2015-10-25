package br.com.vector;

/**
 * Created by Gabriel on 25/10/2015.
 */
public final class Values {

    private static String ipAdrres;

    private static String dummyData = "{\"PlayersDamage\":[{\"ChampionName\":\"Caitlyn\",\"GoldEarned\":3550.00342,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"Nasus\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"Ryze\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"Malphite\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"Malzahar\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"Zilean\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"Morgana\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"KogMaw\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"MissFortune\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0},{\"ChampionName\":\"Vladimir\",\"GoldEarned\":0,\"TotalDamageDealt\":0,\"PhysicalDamageDealt\":0,\"MagicallDamageDealt\":0}],\"EnemyTeamGold\":0,\"AllyTeamGold\":3550.00342}";

    public static String getDummyData() {
        return dummyData;
    }

    public static void setDummyData(String dummyData) {
        Values.dummyData = dummyData;
    }

    public static String getIpAdrres() {
        return ipAdrres;
    }

    public static void setIpAdrres(String ipAdrres) {
        Values.ipAdrres = ipAdrres;
    }
}
