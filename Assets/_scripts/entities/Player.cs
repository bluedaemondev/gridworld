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

        /// <summary>
        /// Metodo para animation
        /// </summary>
        public void Attack()
        {
            this.attackArea.enabled = !this.attackArea.enabled;
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

        }
    }
}
