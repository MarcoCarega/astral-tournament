using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SetupSpawnable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> prefabs = Resources.LoadAll<GameObject>("Prefabs/Components").ToList();
        foreach (GameObject obj in prefabs)
        {
            GetComponent<CustomManager>().spawnPrefabs.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
