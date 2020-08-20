using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SetupVehicle : NetworkBehaviour
{
    private GameObject player;
    private Global global;
    private bool endMatch = false;

    // Start is called before the first frame update

    void Start()
    {
        //ricevo istanze salvate in global;
        global = Global.Instance;
        if(isLocalPlayer)
        {
            //se l'oggetto è quyello che rappresenta il player nella scena locale 
            //Prendi l'oggetto della scena che ha al suo interno la camera, e dove metteremo il veicolo 
            //creato nella zselzione
            GameObject player = GameObject.Find("Player");

            //attacca l'oggetto creato, cioè il prefab associato al NetManager.
            //questi tre settano la posizione e rotazione dell'oggetto Vehicle nel campo Player della scena, come figlio di Player.

            transform.position = player.transform.position;
            Vector3 rotation = transform.rotation.eulerAngles; //rotazione giocatore.
            rotation.y = 180;
            transform.SetParent(player.transform);

        }
    }

    void Update()
    {
        
    }
}