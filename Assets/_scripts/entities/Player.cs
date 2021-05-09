using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace Entities
{
    public class Player : Entity, IAttacker
    {
        [SerializeField] private GameObject bloodPrefab;
        [SerializeField] private Animator _animator;
        [SerializeField] private BoxCollider attackArea;

        [SerializeField] private string attackAnimationName = "Attack";
        public string attackButton = "Fire1";
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
            this._animator.Play(attackAnimationName);
        }

        public void Init()
        {
            this.attackArea.enabled = false;
            this._health = new Health(100);
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
    }
}
