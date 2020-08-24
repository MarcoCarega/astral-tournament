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
    // Server callbacks

    private void Start()
    {
        List<GameObject> spawnObj = Resources.LoadAll<GameObject>("Prefabs").ToList();
        foreach (GameObject obj in spawnObj)
        {
            obj.AddComponent<NetworkIdentity>();
            ClientScene.RegisterPrefab(obj, NetworkHash128.Parse(obj.name));
            spawnPrefabs.Add(obj);
        }
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        print("START SERVER");
        List<GameObject> spawnObj = Resources.LoadAll<GameObject>("Prefabs").ToList();
        /*foreach (GameObject obj in spawnObj)
        {
            obj.AddComponent<NetworkIdentity>();
            ClientScene.RegisterPrefab(obj, NetworkHash128.Parse(obj.name));
            spawnPrefabs.Add(obj);
        }*/
        NetworkServer.RegisterHandler(playerMex, OnCreate);
        //NetworkServer.RegisterHandler(playerMex, OnCreate);
    }

    private void OnCreate(NetworkMessage netMsg)
    {
        PlayerMessage message = netMsg.reader.ReadMessage<PlayerMessage>();
        print("MESSAGE CANNON: " + message.cannon);
        NetworkConnection conn = netMsg.conn;
        GameObject game = Instantiate(playerPrefab) as GameObject;
        game.gameObject.AddComponent<SetupLocalPlayer>();
        NetworkVehicle net = game.GetComponent<NetworkVehicle>();
        passValues(ref net, message);
       
        short id = game.GetComponent<NetworkIdentity>().playerControllerId;
        id++;
        id++;
        NetworkServer.AddPlayerForConnection(conn,game,id);
        //NetworkServer.Spawn(game.gameObject);
    }


    public override void OnServerReady(NetworkConnection conn)
    {
        base.OnServerReady(conn);
        print("READY SERVER");
        //GameObject instance = Instantiate<GameObject>(playerPrefab);
        //short id = instance.AddComponent<NetworkIdentity>().playerControllerId;
        //NetworkServer.Spawn(instance);
    }

    /*public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        NetworkVehicle net = GameObject.Find("NetVehicle").GetComponent<NetworkVehicle>();
        if (player != null) Destroy(player);
        player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.GetComponent<NetworkVehicle>().cannon = net.cannon;
        player.GetComponent<NetworkVehicle>().armor = net.armor;
        player.GetComponent<NetworkVehicle>().engine = net.engine;
        player.GetComponent<NetworkVehicle>().wheel = net.wheel;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }*/


    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
     {
        //base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
        print("SERVER ADD PLAYER");
        PlayerMessage message = extraMessageReader.ReadMessage<PlayerMessage>();
        //NetworkVehicle net = new NetworkVehicle(message);
        Debug.Log("server add with message ");

        //playerPrefab = entityToSpawn [selectedClass];

        GameObject player = Instantiate(playerPrefab) as GameObject;
        player.GetComponent<NetworkVehicle>().cannon = message.cannon;
        player.GetComponent<NetworkVehicle>().armor = message.armor;
        player.GetComponent<NetworkVehicle>().engine = message.engine;
        player.GetComponent<NetworkVehicle>().wheel = message.wheel;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
     }

    private void passValues(ref NetworkVehicle player,PlayerMessage net)
    {
        player.cannon = net.cannon;
        player.armor = net.armor;
        player.engine = net.engine;
        player.wheel = net.wheel;
    }

    public override void OnStartClient(NetworkClient client)
    {
        print("START CLIENT");
        base.OnStartClient(client);
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs");
        foreach(GameObject obj in prefabs)
        {
            obj.AddComponent<NetworkIdentity>();
            ClientScene.RegisterPrefab(obj, NetworkHash128.Parse(obj.name));
        }
    }


    // Client callbacks
    public override void OnClientConnect(NetworkConnection conn)
    {
        //base.OnClientConnect(conn);
        print("CLIENT CONNECT");
        Vehicle vehicle = Global.Instance.GetVehicle();
        PlayerMessage message = new PlayerMessage(vehicle.createNetworkInstance());
        conn.Send(playerMex, message);
        Debug.Log("Connected successfully to server, now to set up other stuff for the client...");
    }

    
}
