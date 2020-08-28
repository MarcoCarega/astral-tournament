using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class PowerUp: NetworkBehaviour
{
    protected abstract void onUsePowerUp();

    public void use()
    {
        onUsePowerUp();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
