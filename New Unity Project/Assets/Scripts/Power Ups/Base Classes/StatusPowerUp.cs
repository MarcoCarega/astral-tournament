using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusPowerUp : PowerUp
{
    protected abstract void OnFollow();

    protected override void onUsePowerUp()
    {
        OnFollow();
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
