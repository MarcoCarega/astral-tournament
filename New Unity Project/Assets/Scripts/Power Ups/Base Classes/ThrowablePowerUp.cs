using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowablePowerUp : PowerUp
{
    public abstract void OnThrow();

    protected override void onUsePowerUp()
    {
        OnThrow();
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
