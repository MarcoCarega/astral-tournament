using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GoToArenaUI : MonoBehaviour
{
    public Assemble astroMachine;
    public Button button;
    public Global global;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        button.onClick.AddListener(onCLick);
    }

    private void onCLick()
    {
        //astroMachine.transform.SetParent(null);
        passStats(ref astroMachine);
        global.addVehicle(astroMachine);
        global.GetVehicle().transform.SetParent(null);
        DontDestroyOnLoad(global.GetVehicle());
        SceneManager.LoadScene("SelectArena",LoadSceneMode.Single);
        //DontDestroyOnLoad(game);
    }

    private void passStats(ref Assemble astroMachine)
    {
        Assemble game = astroMachine;//Instantiate(astroMachine) as Assemble;
        //game.transform.SetParent(null);
        game.attack = astroMachine.attack;
        game.defense = astroMachine.defense;
        game.speed = astroMachine.speed;
        game.acceleration = astroMachine.acceleration;
        game.maneuverability = astroMachine.maneuverability;
        game.blocked = true;
        //return game;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

}
