using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private int maxHp; // временно

        [SerializeField]
        private float sec;

        private ushort x = 0;

        private bool isImmortality = false;
        [SerializeField]
        public List<SpellInfo> spellInfos = new List<SpellInfo>();

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
            foreach (var item in spellInfos)
            {
                SpellList.Add(new Spell(item));
            }
            StartCoroutine(RotateAnim());
        }

        public void Cast()
        {

        }

        public void TakeDMG(int dmg)
        {
            if (!isImmortality)
            {
                if (_hp - dmg > 0) _hp -= dmg;
                else { Death(); return; }
                isImmortality = true;
                StartCoroutine(ShotsImmortality());
            }
        }
        public void Death() // временно
        {
            transform.position = Vector3.zero;
            _hp = maxHp;
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

        public IEnumerator RotateAnim()
        {
            while (true)
            {
                x += 1;
                yield return new WaitForSeconds(sec / 1000000000);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 25f * MathF.Sin((float)x / 100));
            }
        }
    }
}
