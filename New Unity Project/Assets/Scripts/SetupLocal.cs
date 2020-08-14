using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocal : NetworkBehaviour
{
    //public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            GameObject player = new GameObject();
            player.name = "Player";
            Movement move=player.AddComponent<Movement>();
            move.speed = 7;
            move.speedLimit = 10;
            move.angle = 5;
            move.drag = 0.99f;
            Camera camera = new GameObject().AddComponent<Camera>();
            camera.gameObject.name = "Camera";
            Vector3 cameraPos = camera.transform.position;
            cameraPos.x -= 20;
            cameraPos.y += 10;
            camera.transform.position = cameraPos;
            camera.transform.LookAt(transform.position);
            camera.transform.SetParent(player.transform);
            Sound sound = player.AddComponent<Sound>();
            sound.source = GetComponent<Sound>().source;
            transform.SetParent(player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
