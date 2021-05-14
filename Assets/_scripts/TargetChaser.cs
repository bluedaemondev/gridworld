using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetChaser : MonoBehaviour
{
    [SerializeField] private EnemyChaser _enemy;

    [SerializeField] private NavMeshAgent _myAgent;
    [SerializeField] private Transform _target;
    //[SerializeField] private Animator animator;
    [SerializeField] private float _delay = 0.5f;
    
    public void Init(EnemyChaser enemy)
    {
        this._enemy = enemy;
    }

    public void StartChasingTarget(Transform _target = null)
    {
        if (_target != null)
            this._target = _target;

        StartCoroutine(Chase());
    }

    private IEnumerator Chase()
    {
        this._enemy.Move();

        while (true)
        {
            if (_myAgent.SetDestination(_target.position))
            {
                while (_myAgent.pathPending)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(_delay);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
