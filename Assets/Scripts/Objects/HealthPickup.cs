using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public LayerMask interactsWith;

    public float HealAmount = -25f;

    private void OnTriggerEnter(Collider collider)
    {
        if ((interactsWith & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer)
        {

            if (collider.gameObject.layer == 9)
            {
                Entity HitEntity = collider.GetComponent<Entity>();

                HitEntity.TakeDamage(HealAmount);

                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.healPickupSound);

        }
    }   
}
