using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupNetwork : MonoBehaviour
{
    private Global global;
    private NetworkManager netManager;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        netManager = GameObject.Find("NetworkManager").GetComponent<AstralManager>();
        netManager.playerPrefab = setupLocalPlayer();
        /*foreach (GameObject obj in Resources.LoadAll<GameObject>("Prefabs"))
        {
            ClientScene.RegisterPrefab(obj, NetworkHash128.Parse(obj.name));
        }*/
        /*foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            obj.AddComponent<NetworkIdentity>();
            ClientScene.RegisterPrefab(obj, NetworkHash128.Parse(obj.name));
        }*/
    }

    private GameObject setupLocalPlayer()
    {
        Vehicle vehicle = global.GetVehicle();
        NetworkVehicle net = vehicle.createNetworkInstance();
        //net.AddComponent<NetworkIdentity>();
        net.gameObject.AddComponent<SetupLocalPlayer>();
        ClientScene.RegisterPrefab(net.gameObject, NetworkHash128.Parse(net.name));
        return net.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
