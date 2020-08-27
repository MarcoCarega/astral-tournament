﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

//Classe con singleton per tenere in memoria tutte le cose necessarie come il personaggio scelto o il veicolo assemblato
public class Global : MonoBehaviour
{
    public string arena;
    public string character;
    public string mode;
    

    private static Global instance;

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

    internal bool hasArena()
    {
        return arena != string.Empty;
    }


    internal bool hasCharacter()
    {
        return character != null;
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
