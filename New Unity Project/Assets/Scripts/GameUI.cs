﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private Global global;
    private Text health;
    private List<RawImage> powerUps;
    private GameObject status;
    private NetworkVehicle player;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        player = global.player;
        powerUps = new List<RawImage>();
        health = GameObject.Find("Canvas/HP").GetComponent<Text>();
        health.text = player.health + "/" + player.maxHealth;
        for(int i=0;i<4;i++)
        {
            powerUps.Add(GameObject.Find("Canvas/Power Ups/Power Up " + i).GetComponent<RawImage>());
            Color color = powerUps[i].color;
            color.a = 0;
            powerUps[i].color = color;
        }
        status = GameObject.Find("Canvas/Status");
    }

    // Update is called once per frame
    void Update()
    {
        health.text = player.health + "/"+ player.maxHealth;
        for(int i=0;i<player.powerUps.Count;i++)
        {
            powerUps[i].texture = player.powerUps[i].GetComponent<Renderer>().sharedMaterial.mainTexture;
        }
    }
}
