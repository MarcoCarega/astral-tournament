using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BoxPowerUp : NetworkBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(isLocalPlayer)
        {
            NetworkVehicle net = collision.collider.GetComponentInParent<NetworkVehicle>();
            if (net != null) //Il gigante non ha la componente NetworkVehicle, quindi se collide con questo oggetto, il valore di net sarà null
            {
                if (!net.IsPowerUpFull())
                {
                    PowerUp powerUp = ChooseRandomPowerUp(); //DA IMPLEMENTARE: sceglie casualmente un powerUp;
                    //CmdTakePowerUp(collision, powerUp);
                }
                else Destroy(this);
                //Distruggere l'oggetto alla fine;
            }
        }
    }

    private PowerUp ChooseRandomPowerUp()
    {
        int num = Mathf.RoundToInt(Random.Range(0, 13));
        return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Command]
    public void CmdTakePowerUp()
    {
        /*NetworkVehicle net = collision.collider.GetComponentInParent<NetworkVehicle>();
        if(!net.IsPowerUpFull())
        {
            if (!isClient)
            {
               //net.AddPowerUp(powerUp);
            }
           // RpcTakePowerUp(collision,powerUp);
        }
        Destroy(this)*/;
    }

    [ClientRpc]
    public void RpcTakePowerUp()
    {
        /*NetworkVehicle net = collision.collider.GetComponentInParent<NetworkVehicle>();
        if (!net.IsPowerUpFull())
        {
            //net.AddPowerUp(powerUp);
        }
        Destroy(this);*/
    }
}
