using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;


public class MirrorConnSetup : MonoBehaviour

{
    //def variabili
    private Global global; //classe in cui si tengono i GameObject di riferimento scelti dall'utente;
    private NetworkManager nmanager; //network manager da prendere dalla scena.


    // Start is called before the first frame update
    void Start()
    {
        print("START MIRRORCONNSETUP");

        global = Global.Instance; //prende istanza di Global.
        //trovo il componente NManager nella scena.
        nmanager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        nmanager.playerPrefab = setPlayerPrefab();



    }

    private GameObject setPlayerPrefab()
    {
        print("VEICOLO INBZIIO");
        Vehicle macchina = global.GetVehicle().GetComponent<Vehicle>();
        GameObject veicoloDaClient = macchina.createNetworkInstance(); //prende il campo "net" del Vehicle.
        veicoloDaClient.AddComponent<SetupVehicle>();
        ClientScene.RegisterPrefab(veicoloDaClient);
        print("VEICOLO CREATO! " + veicoloDaClient);
        return veicoloDaClient;
    }

    
}
