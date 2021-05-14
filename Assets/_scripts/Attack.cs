using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int _damage;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    IDamageable hitHealth = collision.gameObject.GetComponent<IDamageable>();

    //    if (hitHealth != null)
    //    {
    //        hitHealth.TakeDamage(damage);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        IDamageable hitHealth = other.gameObject.GetComponent<IDamageable>();
        if (hitHealth != null)
        {
            hitHealth.TakeDamage(_damage);
        }
    }
}
