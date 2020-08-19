using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SetupNetworkConnection : MonoBehaviour
{
    private Global global;
    private MatchMaker matchMaker;
    private NetworkManager netManager;
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(GameObject.Find("Astromachine(Clone)"));
        global = Global.Instance;
        //netManager = new GameObject().AddComponent<NetworkManager>();
        //netManager.name = "NetworkManager";

        //global.matchMaker = matchMaker;
        if (global.netManager != null)
        {
            Destroy(global.matchMaker);
            Destroy(global.netManager);
            Destroy(global.lobby);
        }
        //global.lobby = new GameObject("Lobby").AddComponent<NetworkLobbyManager>();
        global.netManager = new GameObject().AddComponent<NetworkManager>();
        //NetworkManagerHUD hud= global.netManager.gameObject.AddComponent<NetworkManagerHUD>();
        //hud.showGUI = false;
        global.netManager.name = "NetworkManager";
        matchMaker = new GameObject().AddComponent<MatchMaker>();
        matchMaker.name = "MatchMaker";
        DontDestroyOnLoad(matchMaker);
        global.matchMaker = matchMaker;
        global.netManager.onlineScene = "VehicleMovement";
        //GameObject game= setupPlayer();
        //print(game);
        //DontDestroyOnLoad(game);
        global.netManager.playerPrefab =setupPlayer();
        //global.netManager.autoCreatePlayer = false;
        //global.netManager = netManager;
    }

    private GameObject setupPlayer()
    {
        Vehicle vehicle = global.GetVehicle().GetComponent<Vehicle>();
        GameObject game = vehicle.createNetworkInstance();
        print(vehicle);
        //vehicle.gameObject.AddComponent<NetworkIdentity>().localPlayerAuthority = true;
        //vehicle.gameObject.AddComponent<NetworkTransform>();
        //game.AddComponent<Rigidbody>();
        //vehicle.gameObject.AddComponent<VehicleMovement>().enabled = false;
        game.AddComponent<SetupLocal>();
        //global.netManager.StartHost();
        ClientScene.RegisterPrefab(game,NetworkHash128.Parse(vehicle.name));
        

        global.netManager.playerPrefab = game;
        foreach (GameObject obj in Resources.LoadAll<GameObject>("Prefabs"))
        {
            if (obj.GetComponent<NetworkIdentity>() == null) obj.AddComponent<NetworkIdentity>();
            ClientScene.RegisterPrefab(obj, NetworkHash128.Parse(obj.name));
        }
        //string host = string.Format("Host started on {0}:{1}", global.netManager.networkAddress, global.netManager.networkPort);
        //print(host);

        return game;//.createNetworkInstance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
