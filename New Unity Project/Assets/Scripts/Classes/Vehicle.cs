using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicle : MonoBehaviour
{
    /*public VehicleComponent cannon;
    public VehicleComponent armor;
    public VehicleComponent engine;
    public VehicleComponent wheel;*/

    /*public Select cannon;
    public Select armor;
    public Select engine;
    public Select wheel;*/

    private GameObject board;

    public bool changed;
    private bool first;
    private Global global;
    public GameObject statsView;

    public float speed=0;
    public float acceleration=0;
    public float attack=0;
    public float defense=0;
    public float maneuverability=0;
    public bool blocked;
    private Dictionary<string, VehicleComponent> set;

    private VehicleComponent cannon;
    private VehicleComponent armor;
    private VehicleComponent engine;
    private VehicleComponent wheel;

    private GameObject net;

    // Start is called before the first frame update
    void Start()
    {
        first = true;
        global = Global.Instance;
        net = GameObject.Find("NetVehicle");
        print(net);
    }

    // Update is called once per frame
    void Update()
    {
        set = Select.getSet();
        if (first)
        {
            Select.changed = true;
            first = false;
        }
        if (Select.changed && !blocked && set.Count==4)
        {
            //set = Select.getSet();
            Destroy(board);
            global.removeVehicle();
            board = createObject(PrimitiveType.Cube, "Board", 5, 1, 7);
            for (int i = 0; i < 4; i++)
            {
                wheel = createWheel(set["wheel"],i);
                wheel.transform.SetParent(board.transform);
            }
            engine = createEngine(set["engine"]);
            engine.transform.SetParent(board.transform);
            armor = createArmor(set["armor"]);
            armor.transform.SetParent(board.transform);
            cannon = createCannon(set["cannon"]);
            cannon.transform.SetParent(board.transform);
            adjustVehicleForUI();
            setStats(set);
            //global.addVehicle(GetComponent<Vehicle>());
            Select.changed = false;
            net = createNetVehicle();
            DontDestroyOnLoad(net);
        }
    }

    private GameObject createNetVehicle()
    {
        GameObject game = GameObject.Find("NetVehicle");
        if (game != null) Destroy(game);
        game = new GameObject("NetVehicle");
        NetworkVehicle net = game.AddComponent<NetworkVehicle>();
        print(game);
        print(cannon);
        net.cannon = cannon.name;
        net.armor = armor.name;
        net.engine = engine.name;
        net.wheel = wheel.name;
        return game;
    }

    private void adjustVehicleForUI()
    {
        //board.AddComponent<Vehicle>();
        //passStats(board.GetComponent<Vehicle>(), this);
        board.transform.position = transform.position;
        board.transform.rotation = transform.rotation;
        board.transform.SetParent(transform);
        board.transform.localScale = 3 * board.transform.localScale;
    }

    private void passStats(Vehicle game,Vehicle astroMachine)
    {
        //Assemble game = astroMachine;//Instantiate(astroMachine) as Assemble;
        //game.transform.SetParent(null);
        game.attack = astroMachine.attack;
        game.defense = astroMachine.defense;
        game.speed = astroMachine.speed;
        game.acceleration = astroMachine.acceleration;
        game.maneuverability = astroMachine.maneuverability;
        //game.block();
        //return game;
    }

    private VehicleComponent createCannon(VehicleComponent vehicleComponent)
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

    private VehicleComponent createEngine(VehicleComponent vehicleComponent)
    {
        VehicleComponent engineInstance = copyObject(vehicleComponent);
        engineInstance.transform.position = new Vector3(0, 0, 0);
        engineInstance.transform.localScale = new Vector3(2, 2, 2);
        engineInstance.transform.Translate(0, engineInstance.transform.localScale.y / 2, -board.transform.localScale.z / 2);
        return engineInstance;
    }

    private VehicleComponent createWheel(VehicleComponent component, int index)
    {
        VehicleComponent wheelInstance=copyObject(component);
        wheelInstance.transform.position = new Vector3(0, 0, 0);
        wheelInstance.transform.rotation = Quaternion.Euler(90, 0, 90);
        Vector3 pos = wheelInstance.transform.position;
        wheelInstance.transform.position = setupPos(board, wheelInstance, pos, index);
        pos.x += board.transform.position.x;
        pos.z += board.transform.position.z;
        return wheelInstance;
    }

    private Vector3 setupPos(GameObject board, VehicleComponent game, Vector3 pos, int i)
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

    private VehicleComponent copyObject(VehicleComponent component)
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

    private void setStats(Dictionary<string,VehicleComponent> set)
    {
        if(set.Count==4)
        {
            attack = set["cannon"].values[0];
            statString(attack, "Attack");
            defense = set["armor"].values[0];
            statString(defense, "Defense");
            speed = set["engine"].values[0];
            statString(speed, "Speed");
            acceleration = set["engine"].values[1];
            statString(acceleration, "Acceleration");
            maneuverability = set["armor"].values[1] + set["wheel"].values[0];
            statString(maneuverability, "Maneuverability");
        }
    }

    private void statString(float stat, string name)
    {
        Text[] comps = statsView.GetComponentsInChildren<Text>();
        foreach (Text comp in comps)
            if (comp.name.Equals(name))
                comp.GetComponentsInChildren<Text>()[1].text = stat.ToString();
    }

    public GameObject createNetworkInstance()
    {
        return net;
    }
}