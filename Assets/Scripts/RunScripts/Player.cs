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

        public static Player ins;

        private void Awake()
        {
            ins = this;
        }

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
            if ((Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical")).magnitude > 1)
            {
                gameObject.transform.position += Vector3.Normalize((Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical"))) * _speed * Time.deltaTime;
            }
            else
            {
                gameObject.transform.position += (Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical")) * _speed * Time.deltaTime;
            }
        }

        public void Update()
        {
            Move();
        }

    }
}
