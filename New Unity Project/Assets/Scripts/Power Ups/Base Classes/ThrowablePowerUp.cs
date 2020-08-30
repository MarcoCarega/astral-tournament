﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowablePowerUp : PowerUp
{
    public abstract void OnThrow(Vector3 force);

    protected override void OnUsePowerUp()
    {
        Vector3 force = CalcThrowVector();
        OnThrow(force);
    }

    private Vector3 CalcThrowVector()
    {
        Vector3 center = center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        //print("CENTER: " + center);
        Vector3 mouseVector = Input.mousePosition - center;
        //print("MOUSE: " + mouseVector);
        Vector3 throwVector = (Vector3.forward + mouseVector.normalized).normalized * mouseVector.magnitude * 10;

        //print("THROW: " + throwVector);
        return throwVector;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
