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
        }
        global.netManager = new GameObject().AddComponent<NetworkManager>();
        global.netManager.name = "NetworkManager";
        matchMaker = new GameObject().AddComponent<MatchMaker>();
        matchMaker.name = "MatchMaker";
        DontDestroyOnLoad(matchMaker);
        global.matchMaker = matchMaker;
        global.netManager.onlineScene = "VehicleMovement";
        global.netManager.playerPrefab = setupPlayer();
        //global.netManager.autoCreatePlayer = false;
        //global.netManager = netManager;
    }

    private GameObject setupPlayer()
    {
        Vehicle vehicle = global.GetVehicle().GetComponent<Vehicle>();
        vehicle.gameObject.AddComponent<NetworkIdentity>().localPlayerAuthority = true;
        vehicle.gameObject.AddComponent<NetworkTransform>();
        vehicle.gameObject.AddComponent<Rigidbody>();
        //vehicle.gameObject.AddComponent<VehicleMovement>().enabled = false;
        vehicle.gameObject.AddComponent<SetupLocal>();
        //global.netManager.StartHost();
        ClientScene.RegisterPrefab(vehicle.gameObject);
        global.netManager.playerPrefab = vehicle.gameObject;
        //string host = string.Format("Host started on {0}:{1}", global.netManager.networkAddress, global.netManager.networkPort);
        //print(host);
        return vehicle.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
