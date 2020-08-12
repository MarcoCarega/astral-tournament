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
        GameObject sample= new GameObject();
        sample.AddComponent<NetworkIdentity>();
        sample.AddComponent<NetworkTransform>();
        global.netManager.StartHost();
        global.netManager.playerPrefab = sample;
        string host = string.Format("Host started on {0}:{1}", global.netManager.networkAddress, global.netManager.networkPort);
        print(host);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
