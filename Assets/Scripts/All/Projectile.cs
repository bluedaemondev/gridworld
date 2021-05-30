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
    [SerializeField]
    private GameObject  myExplosionPrefab;

    void Start()
    {
        myRigidbody.velocity = transform.forward * speed;

        StartCoroutine(DestroyAfterTime());
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(myExplosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter()
    {
        Instantiate(myExplosionPrefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeToDestroy);

        Destroy(this.gameObject);
    }
}
