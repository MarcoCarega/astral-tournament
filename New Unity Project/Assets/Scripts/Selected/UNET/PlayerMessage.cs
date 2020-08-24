
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMessage : MessageBase
{
    public string cannon;
    public string armor;
    public string engine;
    public string wheel;

    public PlayerMessage()
    {

    }

    public PlayerMessage(NetworkVehicle net)
    {
        //NetworkVehicle net = vehicle.GetComponent<NetworkVehicle>();
        cannon = net.cannon;
        armor = net.armor;
        engine = net.engine;
        wheel = net.wheel;
    }

}
