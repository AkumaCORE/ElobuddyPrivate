package br.com.vector;

import org.apache.cordova.*;
import org.json.JSONArray;
import org.json.JSONException;

public class Core extends CordovaPlugin {

    @Override
    public boolean execute(String action, JSONArray data, CallbackContext callbackContext) throws JSONException {
        ConnectionManager con = new ConnectionManager();
        if (action.equals("greet")) {
            if (Values.getIpAdrres() == null || Values.getIpAdrres().length() == 0 && Values.getIpAdrres().equals(data.getString(0))) {
                String name = data.getString(0);
                Values.setIpAdrres(name);
                callbackContext.success("Conected !");
                con.run();
                return true;
            } else {
                String message = "The connection is already done.";
                callbackContext.success(message);
                con.run();
                return true;
            }
        } else if (action.equals("request")) {
            con.run();
            callbackContext.success(Values.getDummyData());
        }
        return false;
    }
}
