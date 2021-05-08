using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetChaser : MonoBehaviour
{
    [SerializeField] private NavMeshAgent myAgent;
    [SerializeField] private Transform target;
    [SerializeField] private Animator animator;
    [SerializeField] private string walkingBoolName = "isWalking";
    [SerializeField] private float delay = 0.5f;

    //void Start()
    //{
    //    StartCoroutine(Chase());
    //}

    public void StartChasingTarget(Transform _target = null)
    {
        if (_target != null)
            this.target = _target;

        StartCoroutine(Chase());
    }

    private IEnumerator Chase()
    {
        animator.SetBool(walkingBoolName, true);

        while (true)
        {
            if (myAgent.SetDestination(target.position))
            {
                while (myAgent.pathPending)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(delay);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
