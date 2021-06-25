using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetChaser : MonoBehaviour
{
    [SerializeField] private Entity _enemy;

    [SerializeField] private NavMeshAgent _myAgent;
    [SerializeField] private Transform _target;
    //[SerializeField] private Animator animator;
    [SerializeField] private float _delay = 0.5f;
    public float midDistAttack = 1f;

    public Entity GetTargetFollow()
    {
        return this._enemy;
    }
    public void ToggleNavMeshAgent(bool newState)
    {
        this._myAgent.enabled = newState;
        Debug.Log(newState ? "chase" : "stay, ragdoll");
    }

    public void Init(Entity enemy)
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
        ((IMovable)this._enemy).Move();

        var entity = _target.GetComponent<IDamageable>();

        while (!this._enemy.IsDead() && !entity.IsDead() && _myAgent.enabled)
        {
            if (_myAgent.SetDestination(_target.position))
            {
                while (_myAgent.pathPending)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(_delay);
                if (Vector3.Distance(transform.position, LevelManager.instance.player.position) <= midDistAttack)
                {
                    ((IAttacker)this._enemy).Attack();
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }

        ((IMovable)this._enemy).StopMoving();

        Debug.Log("revisar aca que pasa si no tengo ragdoll. null?");

        if (this._enemy.GetComponent<IRagdoll>() != null)
            ((IRagdoll)this._enemy).EnableRagdollPhysics();

    }
}
