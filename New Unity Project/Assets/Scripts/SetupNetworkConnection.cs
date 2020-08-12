using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupNetworkConnection : MonoBehaviour
{
    private Global global;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        if (global.netManager != null)
            Destroy(global.netManager);
        global.netManager = new GameObject().AddComponent<NetworkManager>();
        global.netManager.name = "NetworkManager";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
