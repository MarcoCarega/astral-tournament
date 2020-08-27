using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class AstralManager : NetworkManager
{
    public static short playerMex = MsgType.Highest + 1;

    private GameObject player;

    public override void OnStartClient(NetworkClient client)
    {
        List<GameObject> prefabs = Resources.LoadAll<GameObject>("Prefabs").ToList();
        foreach (GameObject obj in prefabs)
            ClientScene.RegisterPrefab(obj);
    }

    public override void OnStartServer()
    {
        List<GameObject> prefabs = Resources.LoadAll<GameObject>("Prefabs").ToList();
        foreach (GameObject obj in prefabs)
            spawnPrefabs.Add(obj);
        NetworkServer.RegisterHandler(playerMex, OnCreate);
    }

    private void OnCreate(NetworkMessage netMsg)
    {
        PlayerMessage message = netMsg.reader.ReadMessage<PlayerMessage>();
        NetworkConnection conn = netMsg.conn;
        GameObject player = Instantiate<GameObject>(playerPrefab);
        NetworkVehicle net = player.GetComponent<NetworkVehicle>();
        /*net.cannon = message.cannon;
        net.armor = message.armor;
        net.engine = message.engine;
        net.wheel = message.wheel;*/
        short id = player.GetComponent<NetworkIdentity>().playerControllerId;
        //id++;
        if (id >= 0) NetworkServer.AddPlayerForConnection(conn, player, id);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        //base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
        PlayerMessage message = extraMessageReader.ReadMessage<PlayerMessage>();
        GameObject player = Instantiate<GameObject>(playerPrefab);
        NetworkVehicle net = player.GetComponent<NetworkVehicle>();
        /*net.cannon = message.cannon;
        net.armor = message.armor;
        net.engine = message.engine;
        net.wheel = message.wheel;*/
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        /*Vehicle vehicle = Global.Instance.GetVehicle();
        PlayerMessage message = new PlayerMessage(vehicle.createNetworkInstance());
        conn.Send(playerMex, message);
        Debug.Log("Connected successfully to server, now to set up other stuff for the client...");
        //GameObject player = Instantiate<GameObject>(playerPrefab);
        short id = playerPrefab.GetComponent<NetworkIdentity>().playerControllerId;
        //id++;
        
        if(id>=0) ClientScene.AddPlayer(conn,id);*/
    }
}
