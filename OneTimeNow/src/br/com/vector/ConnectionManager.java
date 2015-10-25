package br.com.vector;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.Socket;

/**
 * Created by Gabriel on 25/10/2015.
 */
public class ConnectionManager extends Thread {
    public void run () {
            SocketServer();
        }

    private void SocketServer(){
        try{
            String modifiedSentence;
            BufferedReader inFromUser = new BufferedReader(new InputStreamReader(System.in));

            Socket clientSocket = new Socket(Values.getIpAdrres(), 8080);
            DataOutputStream outToServer = new DataOutputStream(clientSocket.getOutputStream());
            BufferedReader inFromServer = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));

            outToServer.writeBytes("READ<EOF>");
            modifiedSentence = inFromServer.readLine();
            System.out.println(modifiedSentence);
            Values.setDummyData(modifiedSentence);
            clientSocket.close();
        }catch (Exception e){
            e.printStackTrace();
        }
    }

}
