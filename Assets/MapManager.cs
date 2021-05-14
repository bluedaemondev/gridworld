using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapGroup
{
    public List<GameObject> parts;
    public bool isPresented;

    public Animator animatorGroup;

}

public class MapManager : MonoBehaviour
{
    [SerializeField]
    List<MapGroup> groupParts;
    [SerializeField]
    private int lastInitializedIndex;

    [SerializeField]
    private string initialAnimationTriggerName = "init";

    public void Init()
    {
        groupParts[lastInitializedIndex].animatorGroup.ResetTrigger(initialAnimationTriggerName);
    }

    // flag reservado -1 para usar el del sistema
    public void PlayInitialAnimation(int optId = -1)
    {

        if (optId == -1)
        {
            groupParts[lastInitializedIndex].animatorGroup.SetTrigger(initialAnimationTriggerName);
            groupParts[lastInitializedIndex].isPresented = true;
        }
        else
        {   // mandar id a mano
            groupParts[optId].animatorGroup.SetTrigger(initialAnimationTriggerName);
            groupParts[optId].isPresented = true;
        }
    }
}
