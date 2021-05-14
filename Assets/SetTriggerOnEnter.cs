using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerOnEnter : MonoBehaviour
{
    public bool destroysAfterTrigger = false;
    public LayerMask interactsWith;

    public string triggerName = "die";

    private void OnTriggerEnter(Collider other)
    {
        if (!Common.GetLayersFromMask(interactsWith).Contains(other.gameObject.layer))
            return;

        switch (triggerName)
        {
            case "die":
                other.GetComponent<IDamageable>().Die();
                break;
        }

        if (destroysAfterTrigger)
            Destroy(this.gameObject);
    }

}
