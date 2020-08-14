using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupNetworkConnection : MonoBehaviour
{
    private Global global;
    private MatchMaker matchMaker;
    private NetworkManager netManager;
    // Start is called before the first frame update
    void Start()
    {
        global = Global.Instance;
        //netManager = new GameObject().AddComponent<NetworkManager>();
        //netManager.name = "NetworkManager";

        //global.matchMaker = matchMaker;
        if (global.netManager != null)
        {
            Destroy(global.matchMaker);
            Destroy(global.netManager);
        }
        global.netManager = new GameObject().AddComponent<NetworkManager>();
        global.netManager.name = "NetworkManager";
        matchMaker = new GameObject().AddComponent<MatchMaker>();
        matchMaker.name = "MatchMaker";
        DontDestroyOnLoad(matchMaker);
        global.matchMaker = matchMaker;
        //global.netManager = netManager;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
