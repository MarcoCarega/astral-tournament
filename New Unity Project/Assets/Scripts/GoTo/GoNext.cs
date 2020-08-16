using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoNext : MonoBehaviour
{
    public Button next;
    private bool done;
    private Global global;
    private Vector3 nextPos;
    public RawImage selectedCharacter;
    public Text story;
    

    // Start is called before the first frame update
    void Start()
    {
        nextPos = next.transform.position;
        next.transform.position = new Vector3(nextPos.x + 50, nextPos.y, nextPos.z);
        next.onClick.AddListener(onClick);
        global = Global.Instance;
    }

    private void onClick()
    {
        Character c = global.GetCharacter();
        c.transform.SetParent(null);
        DontDestroyOnLoad(c);
        SceneManager.LoadScene("BuildVehicle");
    }

    // Update is called once per frame
    void Update()
    {
        if(global.hasCharacter())
        {
            if (!done)
            { 
                next.transform.position = nextPos;
                done = true;
            }
            Character c = global.GetCharacter();
            selectedCharacter.color = new Color(c.color.r, c.color.g, c.color.b, 1);
            story.text = c.charactName;
            //story.enabled = true;
            //selectedCharacter.enabled = true;
            //selectedCharacter.ac
        }
        
        //print(selectedCharacter.color);
    }
}
