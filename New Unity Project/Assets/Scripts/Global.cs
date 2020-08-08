using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    private static Global instance;

    public string mode;

    private Character character;

    private Assemble vehicle;

    private GameObject groupCharacters;

    private GameObject groupVehicles;

    private GameObject groupArenas;

    public Arena arena;

    private static List<GameObject> queue;

    public static Global Instance
    {
        get
        {
            if (instance == null)
                create();
            return instance;
        }
    }

    public void addVehicle(Assemble v)
    {
        if (groupVehicles == null)
            groupVehicles = new GameObject();
        if (groupVehicles.transform.childCount > 0)
        {
            Destroy(groupVehicles);
            groupVehicles = new GameObject();
        }
        this.vehicle = Instantiate(v) as Assemble;
        this.vehicle.transform.SetParent(groupVehicles.transform);
    }

    internal Arena GetArena()
    {
        return arena;
    }

    public void addArena(Arena a)
    {
        if (groupArenas == null)
            groupArenas = new GameObject();
        if (groupArenas.transform.childCount > 0)
        {
            Destroy(groupArenas);
            groupArenas = new GameObject();
        }
        this.arena = Instantiate(a) as Arena;
        this.arena.transform.Translate(this.arena.transform.localScale.x, this.arena.transform.localScale.y, this.arena.transform.localScale.z);
        this.arena.transform.SetParent(groupArenas.transform);
    }

    internal bool hasArena()
    {
        return arena != null;
    }

    public Character GetCharacter()
    {
        return character;
    }

    public Assemble GetVehicle()
    {
        return vehicle;
    }

    public void addCharacter(Character c)
    {
        if (groupCharacters == null)
            groupCharacters = new GameObject();
        //Character tmp = this.character;
        //print(this.character);
        //Character[] objs = GameObject.FindObjectsOfType<Character>();
        //GameObject[] objs = GameObject.FindGameObjectsWithTag("character");
        //if (objs.Length>0)
        // for(int i=0;i<objs.Length;i++)
        if (groupCharacters.transform.childCount > 0)
        {
            Destroy(groupCharacters);
            groupCharacters = new GameObject();
        }
        this.character = Instantiate(c) as Character;
        this.character.transform.SetParent(groupCharacters.transform);
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
