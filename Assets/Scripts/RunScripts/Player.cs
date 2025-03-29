using Assets.Scripts.RunScripts.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public sealed class Player : MonoBehaviour, ICanCast, IDamagable
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _hp;

        public static Player ins;

        public event EventHandler death;

        private void Awake()
        {
            ins = this;
        }

        public void Cast()
        {
        }

        public void TakeDMG(int dmg)
        {
            if (_hp - dmg > 0) _hp -= dmg;
            else Death();
        }
        public void Death()
        {
            death.Invoke(this, new EventArgs());
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
