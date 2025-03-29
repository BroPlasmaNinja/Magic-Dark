using Assets.Scripts.RunScripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public sealed class Player : MonoBehaviour, ICanCast, IDamagable
    {
        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _hp;

        [SerializeField]
        private float sec;

        private bool isImmortality = false;

        public List<Spell> SpellList = new List<Spell>();

        IEnumerator ShotsImmortality()
        {
            yield return new WaitForSeconds(sec);
            isImmortality = false;
            yield break;
        }

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
            if (!isImmortality)
            {
                Debug.Log($"Hp - {_hp - dmg}");
                if (_hp - dmg > 0) _hp -= dmg;
                else { Death(); return; }
                isImmortality = true;
                StartCoroutine(ShotsImmortality());
            }
        }
        public void Death()
        {
            death.Invoke(this, new());
            Debug.Log("You lose");
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
