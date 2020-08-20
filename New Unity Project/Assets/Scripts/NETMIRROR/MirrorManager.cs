using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MirrorManager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler<AstralMessage>(OnCreateVehicle);
    }

    

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Vehicle veicolo = GameObject.Find("/Astromachine(Clone)").GetComponent<Vehicle>();
        // you can send the message here, or wherever else you want
        AstralMessage vehicleMessage = new AstralMessage(veicolo.createNetworkInstance().GetComponent<NetworkVehicle>());
        conn.Send(vehicleMessage);
    }

    private void OnCreateVehicle(NetworkConnection conn, AstralMessage message)
    {
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        GameObject veicoloAssemblato = Instantiate(playerPrefab);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        NetworkVehicle player = veicoloAssemblato.GetComponent<NetworkVehicle>();
        player.engine = message.engine;
        player.cannon= message.cannon;
        player.armor = message.armor;
        player.wheel= message.wheel;

       GameObject finale= player.create();
        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, finale);
    }
}

