using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GoToArenaUI : MonoBehaviour
{
    //public Vehicle astroMachine;
    private Global global;
    public Vehicle vehicle;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        GetComponent<Button>().onClick.AddListener(onClick);
    }

    private void onClick()
    {
        //astroMachine.transform.SetParent(null);
        //passStats(ref astroMachine);
        //global.addVehicle(astroMachine);
        //Vehicle vehicle = global.GetVehicle();
        //print(vehicle);
        vehicle.blocked = true;
        //vehicle.transform.SetParent(null);
        global.addVehicle(vehicle);
        global.GetVehicle().transform.SetParent(null);
        DontDestroyOnLoad(global.GetVehicle());
        SceneManager.LoadScene("SelectArena");
        //DontDestroyOnLoad(game);
    }

    private void passStats(ref Vehicle game)
    {
        //Assemble game = astroMachine;//Instantiate(astroMachine) as Assemble;
        //game.transform.SetParent(null);
        /*game.attack = astroMachine.attack;
        game.defense = astroMachine.defense;
        game.speed = astroMachine.speed;
        game.acceleration = astroMachine.acceleration;
        game.maneuverability = astroMachine.maneuverability;*/
        game.blocked = true;
        //return game;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

}
