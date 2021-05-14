using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string boolName = "TargetsInRange";
    private List<Collider> targetsInRange;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private Transform shootingPoint;
    private Collider currentTarget;

    private void Awake()
    {
        targetsInRange = new List<Collider>();
    }

    private void Update()
    {
        for (int i = 0; i < targetsInRange.Count; i++)
        {
            if (targetsInRange[i] == null)
            {
                targetsInRange.RemoveAt(i);

                if (targetsInRange.Count < 1)
                {
                    animator.SetBool("TargetInRange", false);
                }

                i--;
            }
        }

        if (targetsInRange.Count > 0)
        {
            currentTarget = targetsInRange[0];
            float distanceToCurrentTarget = Vector3.Distance(transform.position, currentTarget.transform.position);
            float distanceToNewTarget;
            for (int i = 1; i < targetsInRange.Count; i++)
            {
                distanceToNewTarget = Vector3.Distance(transform.position, targetsInRange[i].transform.position);
                if (distanceToNewTarget < distanceToCurrentTarget)
                {
                    currentTarget = targetsInRange[i];
                    distanceToCurrentTarget = distanceToNewTarget;
                }
            }

            Vector3 lookAtPosition = currentTarget.transform.position;
            lookAtPosition.y = transform.parent.position.y;

            transform.parent.LookAt(lookAtPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        targetsInRange.Add(other);
        if (targetsInRange.Count > 0)
        {
            animator.SetBool("TargetInRange", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        targetsInRange.Remove(other);
        if (targetsInRange.Count < 1)
        {
            animator.SetBool("TargetInRange", false);
        }
    }

    public void CreateProjectile()
    {
        if (targetsInRange.Count > 0)
        {
            GameObject createdProjectile = Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
            createdProjectile.transform.LookAt(currentTarget.bounds.center);
        }
    }
}