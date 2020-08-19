using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Spawner : NetworkBehaviour
{
    private NetworkManager netManager;
    private NetworkVehicle vehicle;
    // Start is called before the first frame update
    void Start()
    {
        netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        vehicle = GameObject.Find("Astromachine(Clone)/NetVehicle").GetComponent<NetworkVehicle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnVehicle(NetworkVehicle vehicle)
    {

        AstralMessage astral = new AstralMessage(vehicle);
        
        ClientScene.AddPlayer(ClientScene.readyConnection, 0, astral);

    }

    private void OnConnectedToHost()
    {
        SpawnVehicle(vehicle);
        
    }
}
