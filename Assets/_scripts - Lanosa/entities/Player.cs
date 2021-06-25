using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using System;

namespace Entities
{
    public class Player : Entity, IAttacker, IRagdoll
    {
        [SerializeField] private float healthAmount = 100f;
        [SerializeField] private GameObject bloodPrefab;
        [SerializeField] private Animator _animator;
        [SerializeField] private BoxCollider attackArea;

        [SerializeField] private string attackBoolParam = "attack";
        [SerializeField] private string attackAnimationName = "Attack";
        [SerializeField] private string damagedAnimationName = "Damaged";
        [SerializeField] private string dyingAnimationName = "Dying";
        [SerializeField] private string dieTrigger = "die";
        [SerializeField] private PlatformerMovementController controller;
        
        public string attackButton = "Fire1";
        public HealthBar healthBar;

        //public string attackTriggerName = "Attack";

        /// <summary>
        /// Metodo para animation
        /// </summary>
        public void AttackEvent()
        {
            if (!_health.IsDead())
                this.attackArea.enabled = !this.attackArea.enabled;
            else
                this.attackArea.enabled = false;
        }
        public void Attack()
        {
            Debug.Log(this._health.GetHealth());

            if (!this._health.IsDead() && !_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {    //this._animator.SetBool(attackBoolParam, true);
                this._animator.Play(attackAnimationName);
                SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.attackSoundPlayer);
            }
        }

        public void Init()
        {
            this.attackArea.enabled = false;
            this._health = new Health(healthAmount);
            healthBar.SetMaxHealth(healthAmount);
            
            //DisableRagdollPhysics();
        }

        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown(attackButton))
            {
                Attack();
            }
        }

        public void SetAnimState(string animatorStateName, bool value)
        {
            this._animator.SetBool(animatorStateName, value);
        }
        public override float TakeDamage(float value)
        {

            value = base.TakeDamage(value);
            if (!this.IsDead())
            {
                //this._animator.Play(damagedAnimationName);
                healthBar.SetHealth(value);
                this._animator.Play(damagedAnimationName);
                
                this._myRigidbody.AddExplosionForce(3 * value / 4, transform.forward, 2);
                SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.dyingPlayer);
            }
            return value;

            

        }
        public override void Die()
        {
            base.Die();
            healthBar.SetHealth(_health.GetHealth());

            this._animator.SetTrigger(dieTrigger);
            this._animator.SetBool("dead", true);

            this._animator.Play(dyingAnimationName);
            controller.canMove = false;
            SoundManager.instance.PlayEffect(LevelManager.instance.soundAssets.dyingPlayer);

            //EnableRagdollPhysics();
        }

        public void EnableRagdollPhysics()
        {

            if (_myRigidbody != null)
                _myRigidbody.isKinematic = false;

            _animator.enabled = false;

            Component[] components = GetComponentsInChildren(typeof(Transform));

            foreach (Component c in components)
            {
                if (c.TryGetComponent<Rigidbody>(out Rigidbody rb) && rb != this.transform)
                {
                    rb.isKinematic = false;
                }
            }

        }

        public void DisableRagdollPhysics()
        {
            //if (_myRigidbody != null)
            //    _myRigidbody.isKinematic = true;

            _animator.enabled = true;

            Component[] components = GetComponentsInChildren(typeof(Transform));

            foreach (Component c in components)
            {
                if (c.TryGetComponent<Rigidbody>(out Rigidbody rb) && rb != this.transform)
                {
                    rb.isKinematic = true;
                }
            }
        }
        public override void Destroy()
        {
            Debug.Log("lose");
            EffectFactory.instance.ShakeCamera(3f, 1.2f);
            LevelManager.instance.Lose();
        }
    }
}
