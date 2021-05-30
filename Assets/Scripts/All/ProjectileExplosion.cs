using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    [SerializeField]
    private float _damage = 0;
    [SerializeField]
    private float blastRadius = 10f;
    [SerializeField]
    private float explosionForce = 4f;
    [SerializeField]
    private float upforce = 1f;

    private Collider[] hitColliders;
    private void OnCollisionEnter(Collision collision)
    {
       
    }

    private void Explode(Vector3 explosionPoint)
    {
        hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius);

        foreach(Collider hitcol in hitColliders)
        {
            if (hitcol.GetComponent<Rigidbody>() != null)
            {
                hitcol.GetComponent<Rigidbody>().isKinematic = false;
                hitcol.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionPoint, blastRadius, upforce, ForceMode.Impulse);

                IDamageable hitHealth = hitcol.gameObject.GetComponent<IDamageable>();
                if(hitHealth != null)
                {
                    hitHealth.TakeDamage(_damage);
                }
            }
        }
    }
}
