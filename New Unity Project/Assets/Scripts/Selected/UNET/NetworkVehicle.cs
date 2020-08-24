using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkVehicle : NetworkBehaviour
{
    [SyncVar] public string cannon;
    [SyncVar] public string armor;
    [SyncVar] public string engine;
    [SyncVar] public string wheel;

    public float speed;
    public float acceleration;
    public float attack;
    public float defense;
    public float maneuverability;

    public float drag;

    public Vector3 velocity;

    //public Vehicle vehicle;

    private GameObject board;
    public static bool changed;
    private bool done;

    public NetworkVehicle(PlayerMessage message)
    {
        cannon = message.cannon;
        armor = message.armor;
        engine = message.engine;
        wheel = message.wheel;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector3(0, 0, 0);
        drag = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
       if(isLocalPlayer)
        {
            Commands();
        }
    }

    private void Commands()
    {
        if (Input.GetKey(KeyCode.W)) //va avanti
        {
            //forward = 1;
            velocity.z += getVelocity(acceleration);
        }
        else if (Input.GetKey(KeyCode.S)) //va indietro
        {
            //forward = -1;
            velocity.z -= getVelocity(acceleration);
            //vehicle.transform.Translate(velocity);
        }
        else velocity *= (1 - drag);
        if (Input.GetKey(KeyCode.A)) //gira a sinistra (destra se è in retromarcia)
        {
            //curve = -1;
            float angle = -getAngle(velocity.z);
            //transform.Rotate(0, angle, 0);
            CmdRotate(angle);
            //transform.Rotate(0, angle, 0);
        }
        else if (Input.GetKey(KeyCode.D))//gira a destra (sinistra se in retromarcia)
        {
            //curve = 1;
            float angle = getAngle(velocity.z);
            //transform.Rotate(0, -angle, 0);
            CmdRotate(angle);
        }
        if (velocity.z > 0) velocity.z = Mathf.Min(velocity.z, speed);
        else velocity.z = Mathf.Max(velocity.z, -speed);
        print("NO DRAG: " + velocity);
       // velocity *= (1 - drag);
        print("DRAG: "+velocity);
        //transform.Translate(velocity);
        if(velocity.magnitude!=0) CmdMove(velocity);
        //CmdRotate(transform.rotation.eulerAngles);
    }

    private float getAngle(float velocity) //calcolo dell'angolo di rotazione
    {
        /*curve = 0;
        if (velocity > 0) curve = 1;
        else if (velocity < 0) curve = -1;
        else curve = 0;*/
        //print(curve);
        float angle = maneuverability / 10;// * Time.deltaTime;
        return angle;
    }

    private float getVelocity(float acceleration) //calcolo velocità
    {
        return acceleration * 5 * Time.deltaTime;
    }

    [Command]
    public void CmdMove(Vector3 velocity)
    {
        transform.Translate(velocity);
        //velocity *= (1 - drag);
        RpcMove(velocity);
    }

    [ClientRpc]
    public void RpcMove(Vector3 position)
    {
        transform.Translate(velocity);
        //velocity *= (1 - drag);
    }

    [Command]
    public void CmdRotate(float angle)
    {
        transform.Rotate(0, angle, 0);
        RpcRotate(angle);
    }

    [ClientRpc]
    public void RpcRotate(float angle)
    {
        if (isServer)
            transform.Rotate(0,angle,0);
    }

   
}
