﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    private Global global;
    public Vehicle vehicle;
    private Vector3 velocity;
    private int curve;
    //private int forward;
    private float speed;

    [Range(0f, 1f)]
    public float drag;
    private void setupCamera(GameObject cameraPos)
    {
        Camera camera = GetComponentInChildren<Camera>();
        camera.transform.rotation = cameraPos.transform.rotation;
        camera.transform.position = cameraPos.transform.position;//new Vector3(transform.position.x - 50,transform.position.y+50,transform.position.z);
        camera.transform.LookAt(vehicle.transform.position);
    }
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        velocity = new Vector3(0, 0, 0);
        vehicle = global.GetVehicle();
        vehicle.transform.position = transform.position;
        //vehicle.transform.SetParent(transform);
        
        //Vector3 vehiclePos = vehicle.transform.localRotation.eulerAngles;
        //vehiclePos.y += 90;
        //vehicle.transform.localRotation = Quaternion.Euler(vehiclePos);
        //vehicle.transform.rotation = transform.rotation;
        //Vector3 rotation = vehicle.transform.rotation.eulerAngles;
        //rotation.y += 90;
        //vehicle.transform.rotation = Quaternion.Euler(rotation);
        vehicle.transform.localScale *= 0.2f;
        speed = vehicle.speed;
        //vehicle.transform.position = new Vector3(-20,-5,-78);
        //vehicle.transform.SetParent(transform);
        //Vector3 rotation = transform.rotation.eulerAngles;
        //rotation.y -= 90;
        //transform.rotation = Quaternion.Euler(rotation);
        GameObject cameraPos = new GameObject();
        Vector3 pos = transform.position;
        pos.x -= 20;
        pos.y += 20;
        cameraPos.transform.position = pos;
        cameraPos.transform.SetParent(vehicle.transform);
        //cameraPos.transform.position = new Vector3(0, 77, 0);
        cameraPos.transform.position = new Vector3(20,100, 0);
        setupCamera(cameraPos);
    }

    // Update is called once per frame
    void Update()
    {
        float acc = vehicle.acceleration;
        if (Input.GetKey(KeyCode.W))
        {
            //forward = 1;
            velocity.z -= getVelocity(acc);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //forward = -1;
            velocity.z += getVelocity(acc);
            //vehicle.transform.Translate(velocity);
        }
        else velocity *= (1 - drag);
        if (Input.GetKey(KeyCode.A))
        {
            //curve = -1;
            float angle = -getAngle(velocity.z);
            vehicle.transform.Rotate(0, angle, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //curve = 1;
            float angle = getAngle(velocity.z);
            vehicle.transform.Rotate(0, angle, 0);
        }
        else curve = 0;

        //velocity *= (1 - drag);
        velocity.z = (velocity.z > 0) ? Mathf.Min(velocity.z, speed) : Mathf.Max(velocity.z, -speed);
        //print(velocity);
        vehicle.transform.Translate(velocity);
        //velocity.x += getVelocity(acc);
        print(velocity);
        //float angle = getAngle(velocity.x);
        //print(angle);


    }

    private float getAngle(float velocity)
    {
        curve = 0;
        if (velocity > 0) curve = 1;
        else if (velocity < 0) curve = -1;
        else curve = 0;
        print(curve);
        float angle = curve*vehicle.maneuverability / 10;// * Time.deltaTime;
        return angle;
    }

    private float getVelocity(float acceleration)
    {
        return acceleration*5 * Time.deltaTime;
    }
}
