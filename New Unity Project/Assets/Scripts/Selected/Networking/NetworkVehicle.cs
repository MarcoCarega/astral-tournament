﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkVehicle : NetworkBehaviour
{
    [SyncVar] public string cannon;
    [SyncVar] public string armor;
    [SyncVar] public string engine;
    [SyncVar] public string wheel;

    public Vehicle vehicle;

    private GameObject board;
    public static bool changed;

    // Start is called before the first frame update
    void Start()
    {
        //board = createObject(PrimitiveType.Cube, "Board", 5, 1, 7);
    }

    // Update is called once per frame
    void Update()
    {

        //prendi content di wheel, engine ecc. spawna un gameobject con quel nome.

        /*for (int i = 0; i < 4; i++)
        {
            VehicleComponent wheel = createWheel(set["wheel"], i);
            wheel.transform.SetParent(board.transform);
        }
        VehicleComponent engine = createEngine(set["engine"]);
        engine.transform.SetParent(board.transform);
        VehicleComponent armor = createArmor(set["armor"]);
        armor.transform.SetParent(board.transform);
        VehicleComponent cannon = createCannon(set["cannon"]);
        cannon.transform.SetParent(board.transform);*/
        
        //GameObject board = new GameObject("NetVehicle");
        
    }

    public void take(NetworkVehicle net)
    {
        wheel = net.wheel;
        engine = net.engine;
        armor = net.armor;
        cannon = net.cannon;
    }

    public GameObject create()
    {
        //if (changed)
        //{

            Dictionary<string, VehicleComponent> set = new Dictionary<string, VehicleComponent>();
            set.Add("wheel", createObject(wheel));
            set.Add("engine", createObject(engine));
            set.Add("armor", createObject(armor));
            set.Add("cannon", createObject(cannon));
            Destroy(board);
            //board = GameObject.Find("NetVehicle/Board");
            board = vehicle.build(set);
            changed = false;
        //}
        return board;
    }

    private VehicleComponent createObject(string name)
    {
        string newName = name.Replace("(Clone)", "");

        return Resources.Load<GameObject>("Prefabs/" + newName).GetComponent<VehicleComponent>(); //Instantiate(Resources.Load("Prefabs/" + newName) as GameObject).GetComponent<VehicleComponent>();
    }

    private GameObject createObject(PrimitiveType type, string name, int x, int y, int z)
    {
        GameObject game = GameObject.CreatePrimitive(type);
        game.name = name;
        game.transform.localScale = new Vector3(x, y, z);
        return game;
    }
}
