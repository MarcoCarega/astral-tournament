using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Classe con singleton per tenere in memoria tutte le cose necessarie come il personaggio scelto o il veicolo assemblato
public class Global : MonoBehaviour
{
    private static Global instance;

    public string mode;

    private Character character;

    private Vehicle vehicle;

    private GameObject group; //variabile di appoggio che serve per non creare infinite istanze di un gameobject scelto

    private Arena arena;

    private static List<GameObject> queue; //attualmente non usata, era una porva che volevo fare

    public NetworkManager netManager;

    public MatchMaker matchMaker;

    private GameObject player;
    public NetworkLobbyManager lobby;

    public GameObject networkVehicle;

    public static Global Instance
    {
        get
        {
            if (instance == null)
                create();
            return instance;
        }
    }

    public void addVehicle(Vehicle v)
    {
         if (group == null)
             group = new GameObject();
         if (group.transform.childCount > 0)
         {
             Destroy(group);
             group = new GameObject();
         }
         group.name = "Group";
         this.vehicle = Instantiate(v) as Vehicle;
         this.vehicle.transform.rotation = Quaternion.Euler(0, 0, 0);
         //this.vehicle.transform.position = new Vector3(0, 0, 0);
         this.vehicle.transform.SetParent(group.transform);
        //vehicle = v;
    }

    internal void AddPlayer(GameObject player)
    {
        if (this.player != null) Destroy(this.player);
        DontDestroyOnLoad(player);
        this.player = player;
    }

    internal Arena GetArena()
    {
        return arena;
    }

    public void addArena(Arena a)
    {
        if (group == null)
            group = new GameObject();
        if (group.transform.childCount > 0)
        {
            Destroy(group);
            group = new GameObject();
        }
        this.arena = Instantiate(a) as Arena;
        //this.arena.transform.rotation = Quaternion.Euler(0, 0, 0);
        //this.vehicle.transform.position = new Vector3(0, 0, 0);
        this.arena.transform.Translate(this.arena.transform.localScale.x, this.arena.transform.localScale.y, this.arena.transform.localScale.z);
        this.arena.transform.SetParent(group.transform);
    }

    internal void removeVehicle()
    {
        Destroy(this.vehicle);
    }

    internal bool hasArena()
    {
        return arena != null;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public Vehicle GetVehicle()
    {
        return vehicle;
    }

    public void addCharacter(Character c)
    {
        if (group == null)
            group = new GameObject();
        //Character tmp = this.character;
        //print(this.character);
        //Character[] objs = GameObject.FindObjectsOfType<Character>();
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("character");
        //if (objs.Length>0)
        // for(int i=0;i<objs.Length;i++)
        if (group.transform.childCount > 0)
        {
            Destroy(group);
            group = new GameObject();
        }
        this.character = Instantiate(c) as Character;
        //this.character.transform.rotation = Quaternion.Euler(0, 0, 0);
        //this.character.transform.position = new Vector3(0, 0, 0);
        this.character.transform.SetParent(group.transform);
        //this.character.tag = "character";
        //DontDestroyOnLoad(this.character);
    }

    public void add(GameObject game)
    {
        queue.Add(game);
        game.transform.SetParent(instance.transform);
    }

    internal bool hasCharacter()
    {
        return character != null;
    }

    public static GameObject remove()
    {
        GameObject game = queue[0];
        queue.RemoveAt(0);
        return game;
    }

    private static void create()
    {
       instance = new GameObject("Global").AddComponent<Global>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
