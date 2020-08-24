
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    private GameObject player;
    private Global global;
    public NetworkVehicle net;

    [SyncVar] public string cannon;
    [SyncVar] public string armor;
    [SyncVar] public string engine;
    [SyncVar] public string wheel;

    void Start()
    {
        global = Global.Instance;
        net = GetComponent<NetworkVehicle>();
        if (isLocalPlayer)
        {
            player = GameObject.Find("Player");
            SetComponents();
        }
        
    }

    void Update()
    {

    }

    private void SetComponents()
    {
        this.cannon = net.cannon;
        this.armor = net.armor;
        this.engine = net.engine;
        this.wheel = net.wheel;
        CmdSetComponents(net.cannon, net.armor, net.engine, net.wheel);
    }

    [Command]
    public void CmdSetComponents(string cannon, string armor, string engine, string wheel)
    {
       
        this.cannon = cannon;
        this.armor = armor;
        this.engine = engine;
        this.wheel = wheel;
        create();
        RpcSetComponents(cannon, armor, engine, wheel);
        
    }

    [ClientRpc]
    public void RpcSetComponents(string cannon, string armor, string engine, string wheel)
    {
        this.cannon = cannon;
        this.armor = armor;
        this.engine = engine;
        this.wheel = wheel;
        create();
    }

    private void create()
    {
        
        Dictionary<string, VehicleComponent> set = new Dictionary<string, VehicleComponent>();
        set.Add("wheel",Resources.Load<GameObject>("Prefabs/"+wheel).GetComponent<VehicleComponent>());
        set.Add("cannon", Resources.Load<GameObject>("Prefabs/" + cannon).GetComponent<VehicleComponent>());
        set.Add("armor", Resources.Load<GameObject>("Prefabs/" + armor).GetComponent<VehicleComponent>());
        set.Add("engine", Resources.Load<GameObject>("Prefabs/" + engine).GetComponent<VehicleComponent>());
        setStats(set);
        GameObject vehicle=build(set);
        vehicle.transform.SetParent(transform);
    }

    private GameObject build(Dictionary<string, VehicleComponent> set) //assemblaggio effettivo
    {
        //Destroy(board);
        //global.removeVehicle();
        GameObject board = createObject(PrimitiveType.Cube, "Board", 5, 1, 7); //Creazione della board
        for (int i = 0; i < 4; i++) //crea 4 ruote
        {
            VehicleComponent wheel = createWheel(board, set["wheel"], i);
            wheel.transform.SetParent(board.transform);
        }
        VehicleComponent engine = createEngine(board, set["engine"]);
        engine.transform.SetParent(board.transform);
        VehicleComponent armor = createArmor(set["armor"]);
        armor.transform.SetParent(board.transform);
        VehicleComponent cannon = createCannon(board, set["cannon"]);
        cannon.transform.SetParent(board.transform);
        return board;
    }

    private VehicleComponent createCannon(GameObject board, VehicleComponent vehicleComponent)
    {
        VehicleComponent cannonInstance = copyObject(vehicleComponent);
        cannonInstance.transform.position = new Vector3(0, 0, 0);
        cannonInstance.transform.localScale = new Vector3(1, 1, 1);
        cannonInstance.transform.localScale = 2 * cannonInstance.transform.localScale;
        float y = 3.5f * board.transform.localScale.y + 2.5f;
        cannonInstance.transform.Translate(0, y, 0);
        return cannonInstance;
    }

    private VehicleComponent createArmor(VehicleComponent vehicleComponent)
    {
        VehicleComponent armorInstance = copyObject(vehicleComponent);
        armorInstance.transform.position = new Vector3(0, 0, 0);
        armorInstance.transform.localScale = new Vector3(6, 4, 6);
        armorInstance.transform.Translate(0, 0.5f + armorInstance.transform.localScale.y, 0);
        armorInstance.transform.localScale = armorInstance.transform.localScale / 4;
        armorInstance.transform.Rotate(0, 180, 0);
        return armorInstance;
    }

    private VehicleComponent createEngine(GameObject board, VehicleComponent vehicleComponent)
    {
        VehicleComponent engineInstance = copyObject(vehicleComponent);
        engineInstance.transform.position = new Vector3(0, 0, 0);
        engineInstance.transform.localScale = new Vector3(2, 2, 2);
        engineInstance.transform.Translate(0, engineInstance.transform.localScale.y / 2, -board.transform.localScale.z / 2);
        return engineInstance;
    }

    private VehicleComponent createWheel(GameObject board, VehicleComponent component, int index)
    {
        VehicleComponent wheelInstance = copyObject(component);
        wheelInstance.transform.position = new Vector3(0, 0, 0);
        wheelInstance.transform.rotation = Quaternion.Euler(90, 0, 90);
        Vector3 pos = wheelInstance.transform.position;
        wheelInstance.transform.position = setupPos(board, wheelInstance, pos, index);
        pos.x += board.transform.position.x;
        pos.z += board.transform.position.z;
        return wheelInstance;
    }

    private Vector3 setupPos(GameObject board, VehicleComponent game, Vector3 pos, int i) // setta la posizione delle route
    {
        float x = board.transform.localScale.x / 2 + game.transform.localScale.y;
        float z = board.transform.localScale.z / 2 + game.transform.localScale.y;
        if (i == 0)
        {
            pos.x += x;
            pos.z += z;
        }
        else if (i == 1)
        {
            pos.x -= x;
            pos.z += z;
        }
        else if (i == 2)
        {
            pos.x += x;
            pos.z -= z;
        }
        else if (i == 3)
        {
            pos.x -= x;
            pos.z -= z;
        }
        return pos;
    }

    private VehicleComponent copyObject(VehicleComponent component) // copia l'oggetto dato
    {
        return Instantiate(component) as VehicleComponent;
    }

    private GameObject createObject(PrimitiveType type, string name, int x, int y, int z)
    {
        GameObject game = GameObject.CreatePrimitive(type);
        game.name = name;
        game.transform.localScale = new Vector3(x, y, z);
        return game;
    }

    private void setStats(Dictionary<string,VehicleComponent> set) //aggiorna le statistiche
    {
        if(set.Count==4)
        {
            net.attack = set["cannon"].values[0];
            net.defense = set["armor"].values[0];
            net.speed = set["engine"].values[0];
            net.acceleration = set["engine"].values[1];
            net.maneuverability = set["armor"].values[1] + set["wheel"].values[0];
        }
    }
}
