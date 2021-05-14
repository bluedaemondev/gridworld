﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject rocket;
    public Rigidbody myRigidBody;
    public float power = 10.0f;
    public float radius = 5.0f;
    public float upforce = 1.0f;

    private void FixedUpdate()
    {
        if (rocket == enabled)
        {
            Invoke("Detonate", 1);
        }
    }
    void Detonate()
    {
        Vector3 explosionPosition = rocket.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach(Collider hit in colliders)
        {
            myRigidBody = hit.GetComponent<Rigidbody>();

            if(myRigidBody != null)
            {
              myRigidBody.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
            }           
        }
    }


}
