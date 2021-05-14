using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody   myRigidbody;
    [SerializeField]
    private float       speed = 2.5f;
    [SerializeField]
    private float       timeToDestroy = 5f;
    void Start()
    {
        myRigidbody.velocity = transform.forward * speed;

        StartCoroutine(DestroyAfterTime());
    }

    private void OnTriggerEnter()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter()
    {
        Destroy(this.gameObject);
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(this.gameObject);
    }
}
