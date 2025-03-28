using Assets.Scripts.RunScripts.Interfaces;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.RunScripts
{
    public sealed class Player : MonoBehaviour, ICanCast, IDamagable
    {
        [Serializable] private float _speed;

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

    }
}
