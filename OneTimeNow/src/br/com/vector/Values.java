package br.com.vector;

/**
 * Created by Gabriel on 25/10/2015.
 */
public final class Values {

    private static String ipAdrres;

    private static String dummyData = "";

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
