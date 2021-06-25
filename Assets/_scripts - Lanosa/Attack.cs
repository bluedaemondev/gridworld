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
            if (!hitHealth.IsDead())
                StartCoroutine(MicroSlowmo());
        }
    }

    IEnumerator MicroSlowmo()
    {
        Time.timeScale = Random.Range(0.25f, 0.8f);
        EffectFactory.instance.ZoomCamera(4.3f, 0.1f);

        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1;
    }
}
