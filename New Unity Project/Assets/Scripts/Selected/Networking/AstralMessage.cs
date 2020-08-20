using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
//Classe per inviare i componenti del veicolo attraverso un messaggio
public class AstralMessage : MessageBase
{
    public string cannon, armor, engine, wheel;

    public AstralMessage(NetworkVehicle vehicle)
    {
        cannon = vehicle.cannon;
        armor = vehicle.armor;
        engine = vehicle.engine;
        wheel = vehicle.wheel;
    }

    public AstralMessage()
    {
        cannon = "";
        armor = "";
        engine = "";
        wheel = "";
    }
}
