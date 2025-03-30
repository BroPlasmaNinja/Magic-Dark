using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        private ushort x = 0;

        protected EnemyInfo enemyInfo;

        public GameObject CreateObject(Transform tr)
        {
            GameObject objEnemy = new GameObject();
            objEnemy.AddComponent<Enemy>().SetState(enemyInfo);

            var rigidComp = objEnemy.AddComponent<Rigidbody2D>();
            rigidComp.gravityScale = 0;
            rigidComp.freezeRotation = true;

            var boxComp = objEnemy.AddComponent<BoxCollider2D>();
            boxComp.size /= 4;

            var spriteComp = objEnemy.AddComponent<SpriteRenderer>();
            spriteComp.sprite = enemyInfo.sprite;
            spriteComp.color = enemyInfo.color;
            spriteComp.sortingOrder = 3;

            objEnemy.transform.parent = tr;
            objEnemy.tag = "Enemy";
            objEnemy.layer = 9;
            objEnemy.transform.localScale *= 3;

            return objEnemy;
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

        // Временно чтобы заполнять поля через инспектор (Тут был ОЛег)
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
            Destroy(gameObject);
            GameManager.runController.Souls+= 10;
        }

        public void AI()
        {
            gameObject.transform.position -= Vector3.Normalize(gameObject.transform.position-Player.ins.gameObject.transform.position) * enemyInfo.speed * Time.deltaTime;
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
                yield return new WaitForSeconds(1/1000000000);
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 25f * MathF.Sin((float)x/100));
            }
        }
    }
}
