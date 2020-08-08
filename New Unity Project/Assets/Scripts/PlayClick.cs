using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayClick : MonoBehaviour
{
    // Start is called before the first frame update
    public Button button;
    public ModeGame mode;
    private Global global;

    void Start()
    {
        global = Global.Instance;
        if(button!=null)
            button.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        global.mode = getText();
        //DontDestroyOnLoad(mode);
        SceneManager.LoadScene("SelectCharacter", LoadSceneMode.Single);
    }

    private string getText()
    {
        return button.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
