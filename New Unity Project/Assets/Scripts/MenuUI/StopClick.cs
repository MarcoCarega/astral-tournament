using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StopClick : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        if(button!=null)button.onClick.AddListener(onCLick);
    }

    private void onCLick()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
