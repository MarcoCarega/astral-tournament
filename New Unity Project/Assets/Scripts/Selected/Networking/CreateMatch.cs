using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class CreateMatch : MonoBehaviour
{
    private Global global;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        Button button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        InputField textbox = GameObject.Find("Canvas/CreateTextBox").GetComponent<InputField>();
        global.matchMaker.CreateInternetMatch(textbox.text);
        GameObject player = GameObject.Find("Player");
        if(!global.netManager.isNetworkActive)
        {
            player = setupPlayer();
            if(global.netManager.IsClientConnected())
                global.AddPlayer(player);
        }
        else print("The player is already set up");
    }

    private GameObject setupPlayer()
    {
        GameObject player = new GameObject();
        player.name = "Player";
        player.AddComponent<NetworkIdentity>();
        player.AddComponent<NetworkTransform>();
        //global.netManager.StartHost();
        global.netManager.playerPrefab = player;
        string host = string.Format("Host started on {0}:{1}", global.netManager.networkAddress, global.netManager.networkPort);
        print(host);
        return player;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
