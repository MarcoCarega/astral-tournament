using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    private int current;

    public Button left;
    public Button right;
    public RawImage choice;

    public List<Stats> images;

    private Stats selected;

    public static bool changed;

    public Stats getSelected()
    {
        return selected;
    }

    // Start is called before the first frame update
    void Start()
    {
        changed= false;
        current = 0;
        left.onClick.AddListener(() =>
            {
                changed = true;
                current = (current - 1);
                if (current == -1) current = images.Count - 1;
            });
        right.onClick.AddListener(() =>
        {
            changed = true;
            current = (current + 1)%images.Count;
        });
        selected = images[current];
    }
        // Update is called once per frame
    void Update()
    {
        //print(images[current]);
        //changed = true;
        Stats obj = images[current];
        choice.texture = obj.GetComponent<Renderer>().sharedMaterial.mainTexture;
        print(choice.texture);
        selected = images[current];
    }

}
