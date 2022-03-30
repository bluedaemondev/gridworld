using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Summoning Enemy
/// </summary>
public class EnemyGuard : Entity, IAttacker, IMovable
{
    StateMachine _fsm;
    public List<Transform> waypoints;
    public float speed;
    public LayerMask targetMask;
    public Collider target;
    public float viewRadius;
    public float viewAngle;
    public float midDistAttack = 1f;
    public LayerMask obstacleMask;
    private int _currentWaypoint = 0;
    public bool foundTarget = false;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _myAgent;
    [SerializeField] private BoxCollider attackArea;
    [SerializeField] private float _delay = 0.5f;
    [SerializeField] private string walkingBoolName = "isWalking";
    [SerializeField] private string attackAnimationName = "Attack";

    public void Init()
    {
        this._health = new Health(100);

        if (attackArea != null)
        this.attackArea.enabled = false;
  
        LevelManager.instance.SubscribeAliveEntity(this);
        _fsm = new StateMachine();
        //_fsm.AddState(GuardStatesEnum.Die, new DieState(_fsm, this));
        _fsm.AddState(GuardStatesEnum.Patrol, new PatrollingState(_fsm, this));
        _fsm.AddState(GuardStatesEnum.Pursuit, new PursuingState(_fsm, this));
        _fsm.ChangeState(GuardStatesEnum.Patrol);
    }

    public override void Start()
    {
        base.Start();
        Init();
    }

    void Update()
    {
        _fsm.OnUpdate();
    }

    public void Patrol()
    {
        Vector3 dir = waypoints[_currentWaypoint].position - transform.position;
        transform.forward = dir;
        transform.position += transform.forward * speed * Time.deltaTime;

        if (dir.magnitude < 0.1f)
        {
            _currentWaypoint++;
            if (_currentWaypoint > waypoints.Count - 1)
                _currentWaypoint = 0;
        }
    }

    public void FieldOfView()
    {
        Collider[] allTargets = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        foreach (var item in allTargets)
        {
            Vector3 dir = item.transform.position - transform.position;

            if (Vector3.Angle(transform.forward, dir.normalized) < viewAngle / 2)
            {
                if(Physics.Raycast(transform.position, dir, out RaycastHit hit, dir.magnitude, obstacleMask) == false)
                {

                    target = item;
                    foundTarget = true;

                }
                else
                {

                }
            }
        }
    }

    public override float TakeDamage(float value)
    {
        SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.dyingEnemyGuard);
        return base.TakeDamage(value);
    }

    public override void Die()
    {
        base.Die();
        //_fsm.ChangeState(GuardStatesEnum.Die);
        this._animator.SetTrigger("die");
        SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.dyingEnemyGuard);

        LevelManager.instance.RemoveEnemyFromAccountance(this);

        Destroy(this.gameObject, 3f);

    }

    public void Attack()
    {
        if (!this._health.IsDead())
            SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.attackSoundGuard);
            //this._animator.SetBool(attackBoolParam, true);
            this._animator.Play(attackAnimationName);
    }
    
    public void AttackEvent()
    {
        if (!_health.IsDead())
            this.attackArea.enabled = !this.attackArea.enabled;
        else
            this.attackArea.enabled = false;
    }

    public void StartChasingTarget(Transform _target = null)
    {
        StartCoroutine(Chase());
    }

    private IEnumerator Chase()
    {
        Move();

        var entity = target.GetComponent<IDamageable>();

        while (!IsDead() && !entity.IsDead() && _myAgent.enabled)
        {
            if (_myAgent.SetDestination(target.transform.position))
            {
                while (_myAgent.pathPending)
                {
                    yield return new WaitForEndOfFrame();
                }

                yield return new WaitForSeconds(_delay);
                if (Vector3.Distance(transform.position, LevelManager.instance.player.position) <= midDistAttack)
                {
                    Attack();
                }
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }

        StopMoving();
    }

    public void ToggleNavMeshAgent(bool newState)
    {
        this._myAgent.enabled = newState;
        Debug.Log(newState ? "chase" : "stay, ragdoll");
    }

    public void Move()
    {
        this._animator.SetBool(walkingBoolName, true);
    }

    public void StopMoving()
    {
        this._animator.SetBool(walkingBoolName, false);
    }

    public void DeactivateNavMesh()
    {
        ToggleNavMeshAgent(false);
    }

    public void BeginChase()
    {
        StartChasingTarget(LevelManager.instance.player);
    }

    public void Run()
    {
        this._animator.SetTrigger("isRunning");
    }
}
