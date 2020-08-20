using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Prova
//Classe che spawna i giocatori che si connettono

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

    void SpawnVehicle(NetworkVehicle vehicle) //Spawna il veicolo
    {

        AstralMessage astral = new AstralMessage(vehicle);
        
        ClientScene.AddPlayer(ClientScene.readyConnection, 0, astral);

    }

    private void OnConnectedToServer() //callback eseguita quando un client si connette al server
    {
        SpawnVehicle(vehicle);
    }
}
