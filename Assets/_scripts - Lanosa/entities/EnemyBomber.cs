using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomber : Entity, IAttacker, IMovable, IExplodable
{
    public static EnemyBomber InstantiateEnemy(Vector3 position, Quaternion rot, Transform enemyContainerScene, EnemySpawner spawnPoint)
    {
        var go = Instantiate(LevelManager.instance.assets.enemyBomber_Prefab, position, rot, enemyContainerScene);
        var result = go.GetComponent<EnemyBomber>();
        result._originSpawner = spawnPoint;
        return result;
    }

    EnemySpawner _originSpawner;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Animator _animator;
    [SerializeField] private SphereCollider attackArea;

    [SerializeField] private TargetChaser _chaser;

    [SerializeField] private string danceTriggerName = "dance";
    [SerializeField] private string deadTriggerName = "die";
    [SerializeField] private string walkingBoolName = "isWalking";
    [SerializeField] private string jumpingBoolName = "isJumping";
    [SerializeField] private string startAttackBoolName = "targetInRange";

    public bool canMove = true;
    public int stepSoundChance = 15;

    //public float timeExplode = 4;
    public float distanceMinExplode = 5;
    public float distanceMaxLoseSight = 8;

    public void Init()
    {
        this._health = new Health(25);

        if (attackArea != null)
            this.attackArea.enabled = false;

        if (_chaser == null)
            _chaser = GetComponent<TargetChaser>();

        if (_animator == null)
            _animator = GetComponent<Animator>();

        LevelManager.instance.SubscribeAliveEntity(this);

        _chaser.Init(this);
        _chaser.StartChasingTarget(LevelManager.instance.player);
    }

    public void Attack()
    {
        if (!this._health.IsDead())
            _animator.SetBool(startAttackBoolName, true);
    }
    public void Explode()
    {
        Debug.Log("explosion");
        EffectFactory.instance.ZoomCamera(-2, 0.05f);
        EffectFactory.instance.InstantiateEffectAt(explosionPrefab, transform.position, transform.rotation, LevelManager.instance.enemiesContainer);

        Destroy(this.gameObject);
    }

    public bool CheckTargetStillInRangeMB()
    {
        float distance = 0;
        distance = Common.DistanceBetweenPoints(_chaser.GetTargetFollow().transform.position, transform.position);

        return distance < distanceMaxLoseSight;
    }

    public void Move()
    {
        this._animator.SetBool(walkingBoolName, true);
    }

    public void StopMoving()
    {
        this._animator.SetBool(walkingBoolName, false);
    }

    public void SoundStep()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand < stepSoundChance)
            SoundManager.instance.PlayAmbient(LevelManager.instance.soundAssets.stepPlayer);
    }

    public override void Start()
    {
        base.Start();
        Init();
    }

}
