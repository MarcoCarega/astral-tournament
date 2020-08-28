using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    private CustomManager netManager;

    [SyncVar] public int cannon; //Componenti
    [SyncVar] public int armor;
    [SyncVar] public int engine;
    [SyncVar] public int wheel;

    public float speed; //Statistiche
    public float acceleration;
    public float attack;
    public float defense;
    public float maneuverability;

    private Vector3 velocity; //Variabili per movimento
    private float drag;

    // Start is called before the first frame update
    void Start()
    {
        initMovementThings();
        netManager = GameObject.Find("NetworkManager").GetComponent<CustomManager>();
        if (isLocalPlayer)
        {
            print("LOCAL CLIENT!");
            NetworkVehicle net = GetComponent<NetworkVehicle>();
            CmdSetComponents(net.cannon, net.armor, net.engine, net.wheel);
        }
        else if (!hasAuthority)
        {
            createVehicle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            MoveCommands();
        }
    }

    //Comandi e RPC

    [Command]
    public void CmdSetComponents(int cannon, int armor, int engine, int wheel)
    {
        this.cannon = cannon;
        this.armor = armor;
        this.engine = engine;
        this.wheel = wheel;
        print("CreateVehicle() Command...");
        if (!isClient) createVehicle();
        RpcSetComponents(cannon, armor, engine, wheel);
    }

    [ClientRpc]
    public void RpcSetComponents(int cannon, int armor, int engine, int wheel)
    {
        print("CANNONS: " + this.cannon + "----" + cannon);
        this.cannon = cannon;
        print("CANNON DOPO:" + this.cannon);
        this.armor = armor;
        this.engine = engine;
        this.wheel = wheel;
        print("createVehicle RPC...");
        createVehicle();
    }

    [Command]
    public void CmdRotate(float angle)
    {
        if (!isClient)
        {
            transform.Rotate(0, angle, 0);
        }
        RpcRotate(angle);
    }

    [ClientRpc]
    public void RpcRotate(float angle)
    {
         transform.Rotate(0, angle, 0);
    }

    [Command]
    public void CmdTranslate(Vector3 velocity)
    {
        if(!isClient)
        {
            transform.Translate(velocity);
        }
        RpcTranslate(velocity);
    }

    [ClientRpc]
    public void RpcTranslate(Vector3 velocity)
    {
        transform.Translate(velocity);
    }

    //Funzioni supporto

    private void initMovementThings()
    {
        velocity = Vector3.zero;
        drag = 0.02f;
    }

    private void MoveCommands()
    {
        float dump = 1 - drag;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.z -= GetVelocity();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            velocity.z += GetVelocity();
        }
        else velocity.z *= dump;
        if (Input.GetKey(KeyCode.A))
        {
            CmdRotate(-GetAngle());
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CmdRotate(GetAngle());
        }
        velocity.z = ResizeVelocity(velocity.z);
        CmdTranslate(velocity);
    }

    private float GetVelocity()
    {
        return acceleration * Time.deltaTime;
    }

    private float GetAngle()
    {
        return maneuverability;
    }

    private float ResizeVelocity(float velocity)
    {
        return (velocity > 0) ? Mathf.Min(velocity, speed) : Mathf.Max(velocity, -speed);
    }

    private void createVehicle()
    {
       // netManager = GameObject.Find("NetworkManager").GetComponent<CustomManager>();
        List<GameObject> prefab = netManager.spawnPrefabs;
        print("WHEEL: " + prefab[wheel]);
        Dictionary<string, VehicleComponent> set = new Dictionary<string, VehicleComponent>();
        set.Add("wheel", prefab[wheel].GetComponent<VehicleComponent>());
        set.Add("cannon", prefab[cannon].GetComponent<VehicleComponent>());
        set.Add("armor", prefab[armor].GetComponent<VehicleComponent>());
        set.Add("engine", prefab[engine].GetComponent<VehicleComponent>());
        setStats(set);
        GameObject vehicle = build(set);
        vehicle.transform.SetParent(transform);
        print("CreateVehicle Succeeded!");
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

    private void setStats(Dictionary<string,VehicleComponent> set)
    {
        speed = set["engine"].values[0];
        acceleration = set["engine"].values[1];
        attack = set["cannon"].values[0];
        defense = set["armor"].values[0];
        maneuverability = set["armor"].values[1] + set["wheel"].values[0];
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
        print("COMPONENT: " + component);
        return Instantiate(component) as VehicleComponent;
    }

    private GameObject createObject(PrimitiveType type, string name, int x, int y, int z)
    {
        GameObject game = GameObject.CreatePrimitive(type);
        game.name = name;
        game.transform.localScale = new Vector3(x, y, z);
        return game;
    }

}
