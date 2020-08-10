using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSelected : MonoBehaviour
{
    public Button buy;
    public Text money;
    private Vector3 buyPos;
    private bool done;


    // Start is called before the first frame update
    void Start()
    {
        done = false;
        buy.onClick.AddListener(onClick);
        buyPos = buy.transform.position;
        buy.transform.position = new Vector3(buyPos.x + 70, buyPos.y, buyPos.z);
    }

    private void onClick()
    {
        float price = Tile.Selected.price;
        float tot = int.Parse(money.text);
        float newTot = tot - price;
        if (newTot >= 0)
        {
            money.text = newTot.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Tile.Selected!=null && !done)
        {
            buy.transform.position = buyPos;
            done = true;
        }
    }
}
