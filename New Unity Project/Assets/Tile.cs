using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Purchasable componentVehicle;
    public RawImage image;
    public Text text;
    private static Purchasable selected;
    public Button button;

    public static Purchasable Selected
    {
        get
        {
            return selected;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(onClick);
    }

    public void onClick()
    {
        selected = componentVehicle;
    }

    // Update is called once per frame
    void Update()
    {
        if(selected!=null)
        {
            Material m = selected.component.GetComponent<Renderer>().sharedMaterial;
            image.texture = m.mainTexture;
            text.text = selected.name;
        }
    }
}
