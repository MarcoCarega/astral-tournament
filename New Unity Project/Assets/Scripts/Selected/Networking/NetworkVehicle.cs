using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkVehicle : NetworkBehaviour
{
    [SyncVar] public string cannon;
    [SyncVar] public string armor;
    [SyncVar] public string engine;
    [SyncVar] public string wheel;

    private GameObject board;
    // Start is called before the first frame update
    void Start()
    {
        board = createObject(PrimitiveType.Cube, "Board", 5, 1, 7);
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
    }

    private GameObject createObject(PrimitiveType type, string name, int x, int y, int z)
    {
        GameObject game = GameObject.CreatePrimitive(type);
        game.name = name;
        game.transform.localScale = new Vector3(x, y, z);
        return game;
    }
}
