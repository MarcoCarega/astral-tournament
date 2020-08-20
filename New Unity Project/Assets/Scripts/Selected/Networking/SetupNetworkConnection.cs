using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
//Script che prepata tutto il necessario per il networking
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
        global.netManager = new GameObject().AddComponent<NetworkManager>(); //creazione NetworkManager (qui in versione custom)
        global.netManager.gameObject.AddComponent<NetworkManagerHUD>();
        //NetworkManagerHUD hud= global.netManager.gameObject.AddComponent<NetworkManagerHUD>();
        //hud.showGUI = false;
        // Per spawnare a comando metti a false: global.netManager.autoCreatePlayer = false;
        global.netManager.name = "NetworkManager";
        matchMaker = new GameObject().AddComponent<MatchMaker>(); //creazione matchmaker
        matchMaker.name = "MatchMaker";
        DontDestroyOnLoad(matchMaker);
        global.matchMaker = matchMaker;
        global.netManager.onlineScene = "VehicleMovement";
        //GameObject game= setupPlayer();
        //print(game);
        //DontDestroyOnLoad(game);
        global.netManager.playerPrefab =setupPlayer(); //passa al network manager il prefab del giocatore
        //global.netManager.autoCreatePlayer = false;
        //global.netManager = netManager;
    }

    private GameObject setupPlayer() //crea il prefab del player
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
        ClientScene.RegisterPrefab(game,NetworkHash128.Parse(vehicle.name)); //registra il prefab del player
        

        global.netManager.playerPrefab = game;
        foreach (GameObject obj in Resources.LoadAll<GameObject>("Prefabs")) //registra i prefab dei componenti, soluzione che apparentemente funziona solo se l'editor è client e la build è host 
        {                                                                    //(con 2 editor non funziona, dice che non riesce a trovarli)
            print(obj.name);
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
