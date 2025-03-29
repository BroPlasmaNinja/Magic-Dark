using Assets.Scripts.RunScripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public sealed class Player : MonoBehaviour, ICanCast, IDamagable
    {
        [SerializeField]
        private float _speed;

        public event EventHandler death;

        public void Cast()
        {

        }

        public void TakeDMG()
        {

        }
        public void Death()
        {

        }

        public void Move()
        {
            gameObject.transform.position += new Vector3(0, _speed * Input.GetAxis("Vertical"), 0);
            gameObject.transform.position += new Vector3(_speed * Input.GetAxis("Horizontal"), 0, 0);
        }

        public void Update()
        {
            Move();
        }

    }
}
