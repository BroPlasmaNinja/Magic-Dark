using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;
using UnityEngineInternal;

namespace Assets.Scripts.RunScripts
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private float sec;

        private ushort x = 0;

        protected EnemyInfo enemyInfo;

        // Неизменяемый EnemyInfo

        public GameObject CreateObject(Transform tr)
        {
            GameObject objEnemy = new GameObject();
            objEnemy.AddComponent<Enemy>().SetState(enemyInfo);

            objEnemy.AddComponent<SpriteRenderer>().sprite = enemyInfo.sprite;

            var rigidComp = objEnemy.AddComponent<Rigidbody2D>();
            rigidComp.gravityScale = 0;
            rigidComp.freezeRotation = true;

            objEnemy.AddComponent<Collider2D>();

            objEnemy.transform.parent = tr;

            return Instantiate(objEnemy, objEnemy.transform);
        }

        public Enemy(EnemyInfo enemyInfo)
        {
            this.enemyInfo = new EnemyInfo();
            this.enemyInfo.SetState(enemyInfo);
        }

        public void SetState(EnemyInfo enemyInfo)
        {
            this.enemyInfo = new EnemyInfo();
            this.enemyInfo.SetState(enemyInfo);
        }

        // Временно чтобы заполнять поля через инспектор
        protected void Awake()
        {
            StartCoroutine(RotateAnim());
        }

        public event EventHandler death;

        public void TakeDMG(int dmg)
        {
            if (enemyInfo.hp - enemyInfo.baseDmg > 0) enemyInfo.hp -= enemyInfo.baseDmg;
            else Death();
        }

        public void Death()
        {
            death.Invoke(this, new EventArgs());
            Debug.Log("You win");
        }

        public void AI()
        {
            gameObject.transform.position += Vector3.Normalize(Player.ins.transform.position - gameObject.transform.position) * enemyInfo.speed * Time.deltaTime;
        }

        public void Update()
        {
            AI();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.ins.TakeDMG(enemyInfo.baseDmg);
            }
        }

        public IEnumerator RotateAnim()
        {
            while (true)
            {
                x += 1;
                yield return new WaitForSeconds(sec/1000000000);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 25f * MathF.Sin((float)x/10));
            }
        }
    }
}
