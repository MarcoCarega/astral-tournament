using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AstralNetwork : NetworkManager
{
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        //public override void OnServerAddPlayer(UnityEngine.Networking.NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader){
        base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);

        AstralMessage message = extraMessageReader.ReadMessage<AstralMessage>();
        
        Debug.Log("Cannon: " + message.cannon);

        //playerPrefab = entityToSpawn [selectedClass];

        GameObject player = Instantiate<GameObject>(Resources.Load<GameObject>(message.cannon));

        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        //NetworkServer.AddPlayerForConnection (conn, entityToSpawn [selectedClass], playerControllerId);


        //MasterSceneManager.staticMasterSceneManager.SetTime();
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
