using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMapGroup : MonoBehaviour
{
    public int id;

    [SerializeField]
    LayerMask interactsWith;

    [SerializeField]
    LevelManager levelSceneManager;


    private void OnTriggerEnter(Collider other)
    {
        if (!Common.GetLayersFromMask(interactsWith).Contains(other.gameObject.layer))
            return;

        levelSceneManager.mapManager.PlayInitialAnimation(this.id);

        Destroy(this.gameObject);
    }

}
