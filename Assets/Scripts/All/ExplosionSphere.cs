using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSphere : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 0.09f;
    [SerializeField] private float Damage = 15f;

    void Start()
    {
        Destroy(this.gameObject, timeToDestroy);  
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IDamageable>().TakeDamage(Damage);
    }
}
