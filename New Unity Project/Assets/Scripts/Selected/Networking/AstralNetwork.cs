using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
//NetworkManager custom
public class AstralNetwork : NetworkManager
{
    //funzione eseguita quando un server aggiunge un giocatore, 'soluzione' trovata sul forum di unity chiesta da uno con un problema simile al nostro
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        //public override void OnServerAddPlayer(UnityEngine.Networking.NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader){
        base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);

        AstralMessage message = extraMessageReader.ReadMessage<AstralMessage>();
        
        Debug.Log("Cannon: " + message.cannon); //per adesso la prova è stata fatta solo sul cannone

        //playerPrefab = entityToSpawn [selectedClass];

        GameObject player = Instantiate<GameObject>(Resources.Load<GameObject>(message.cannon));

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //NetworkServer.AddPlayerForConnection (conn, entityToSpawn [selectedClass], playerControllerId);


        //MasterSceneManager.staticMasterSceneManager.SetTime();  //Queste non so a che servano, erano nello script del forum
        //MasterSceneManager.staticMasterSceneManager.UpdateItemsForNewPlayer();
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
