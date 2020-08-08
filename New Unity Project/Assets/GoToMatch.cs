using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToMatch : MonoBehaviour
{
    public Button next;

    private Global global;
    private Vector3 nextPos;
    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        done = false;
        nextPos = next.transform.position;
        next.transform.position = new Vector3(nextPos.x + 50, nextPos.y, nextPos.z);
        global = Global.Instance;
        next.onClick.AddListener(onClick);
    }

    private void onClick()
    {
        Arena arena = global.GetArena();
        arena.transform.SetParent(null);
        DontDestroyOnLoad(arena);
        SceneManager.LoadScene("VehicleSample");
    }

    // Update is called once per frame
    void Update()
    {
        if(global.hasArena() && !done)
        {
            next.transform.position = nextPos;
            done = true;
        }
    }
}
