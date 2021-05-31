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
    public float midDistAttack = 1f;
    
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

        var entity = _target.GetComponent<IDamageable>();

        while (!this._enemy.IsDead() && !entity.IsDead())
        {
            if (_myAgent.SetDestination(_target.position))
            {
                while (_myAgent.pathPending)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(_delay);
                if(Vector3.Distance(transform.position, LevelManager.instance.player.position) <= midDistAttack)
                {
                    this._enemy.Attack();
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }

        this._enemy.StopMoving();
        this._enemy.EnableRagdollPhysics();

    }
}
