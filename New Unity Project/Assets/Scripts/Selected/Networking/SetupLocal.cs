using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocal : NetworkBehaviour
{
    private GameObject player;
    private Global global;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        if (isLocalPlayer)
        {
            GameObject player = GameObject.Find("Player");
            if (player == null) player = setupPlayer();
            attachPlayer(ref player);

        }
    }

    private void attachPlayer(ref GameObject player)
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        
    }

    private GameObject setupPlayer()
    {
        GameObject player = new GameObject();
        player.name = "Player";
        VehicleMovement move = player.AddComponent<VehicleMovement>();
        //setupMovement(ref move);
        player.AddComponent<PlayerStatus>();
        Camera camera = setupCamera();
        camera.transform.SetParent(transform);
        transform.SetParent(player.transform);
        print(player.transform.childCount);
        return player;
    }

   
    private Camera setupCamera()
    {
        Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //camera.gameObject.name = "Camera";
        Vector3 cameraPos =camera.transform.position;
        cameraPos.x -= 20;
        cameraPos.y += 10;
        cameraPos.z = -cameraPos.z;
        camera.transform.position = cameraPos;
        camera.transform.LookAt(transform.position);
        return camera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
