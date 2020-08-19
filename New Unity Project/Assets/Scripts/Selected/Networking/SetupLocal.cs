using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocal : NetworkBehaviour
{
    

    private GameObject player;
    private Global global;
    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        if (isLocalPlayer)
        {
            GameObject player = GameObject.Find("Player");
            /*if (player == null) player = setupPlayer();
            attachPlayer(ref player);*/
            transform.position = player.transform.position;
            Vector3 rotation = transform.rotation.eulerAngles;
            rotation.y = 180;
            transform.rotation = Quaternion.Euler(rotation);
            transform.SetParent(player.transform);
        
        }
        //GameObject vehicle = GetComponent<NetworkVehicle>().create();
        //vehicle.transform.SetParent(transform);
        
    }

    private void attachPlayer(ref GameObject player)
    {
        //transform.position = player.transform.position;
        //transform.rotation = player.transform.rotation;
        transform.SetParent(player.transform);
        setupCamera();

    }

    private GameObject setupPlayer()
    {
        
        GameObject player = new GameObject();
        //GetComponent<NetworkVehicle>().create();
        player.name = "Player";
        Vector3 position = player.transform.position;
        position.y = 56;
        player.transform.position = position;
        VehicleMovement move = player.AddComponent<VehicleMovement>();
        //setupMovement(ref move);
        player.AddComponent<PlayerStatus>();
        Camera camera = setupCamera();
        camera.transform.SetParent(transform);
        transform.SetParent(player.transform);
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = 180;
        transform.rotation = Quaternion.Euler(rotation);
        transform.position=player.transform.position;
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
        if(!done && isLocalPlayer)
        {
            GameObject game = global.networkVehicle;
            print(game);
            NetworkVehicle net = game.GetComponent<NetworkVehicle>();
            GetComponent<NetworkVehicle>().take(net);
            GameObject netVehicle = GetComponent<NetworkVehicle>().create();
            netVehicle.transform.SetParent(transform);
            done = true;
        }
    }

}
