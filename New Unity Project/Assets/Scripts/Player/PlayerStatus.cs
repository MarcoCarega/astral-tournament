using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount != 2)
            GetComponent<VehicleMovement>().enabled = false;
        else GetComponent<VehicleMovement>().enabled = true;
    }
}
