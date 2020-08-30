using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralBomb : ThrowablePowerUp
{
    public float force;
    public float radius;

    public override void OnThrow(Vector3 force)
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.AddForce(force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        Vector3 position = transform.position;
        rigid.AddExplosionForce(force, position, radius);
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
