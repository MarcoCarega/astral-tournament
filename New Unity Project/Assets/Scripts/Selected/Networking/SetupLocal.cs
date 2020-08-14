using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocal : NetworkBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            GameObject player = GameObject.Find("Player");
            if (player == null) player = setupPlayer();
            else attachPlayer(ref player);
        }
    }

    private void attachPlayer(ref GameObject player)
    {
        transform.position = player.transform.position;
        transform.rotation = player.transform.rotation;
        transform.SetParent(player.transform);
    }

    private GameObject setupPlayer()
    {
        GameObject player = new GameObject();
        player.name = "Player";
        Movement move = player.AddComponent<Movement>();
        move.speed = 7;
        move.speedLimit = 10;
        move.angle = 5;
        move.drag = 0.99f;
        player.AddComponent<PlayerStatus>();
        Camera camera = new GameObject().AddComponent<Camera>();
        camera.gameObject.name = "Camera";
        Vector3 cameraPos = camera.transform.position;
        cameraPos.x -= 20;
        cameraPos.y += 10;
        camera.transform.position = cameraPos;
        camera.transform.LookAt(transform.position);
        camera.transform.SetParent(player.transform);
        transform.SetParent(player.transform);
        return player;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
