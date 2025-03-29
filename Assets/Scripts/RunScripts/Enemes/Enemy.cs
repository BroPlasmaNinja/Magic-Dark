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
        // это временно для тестов (начало)
        // Или оставить чтобы балансить в будущем? Или по другой какой причеине может.
        [SerializeField]
        private float _speed;

        [SerializeField]
        private int _hp;

        [SerializeField]
        private int _baseDmg;

        [SerializeField]
        private float sec;
        // это временно для тестов (конец)

        private EnemyType _enemyType;

        private ushort x = 0;

        protected EnemyInfo _enemyInfo;

        // Неизменяемый EnemyInfo
        public EnemyInfo _baseEnemyInfo { get; private set; }

        public static GameObject CreateObject(Transform tr, Enemy en)
        {
            return Instantiate(en.gameObject, tr);
        }

        public Enemy(EnemyInfo enemyInfo)
        {
            _baseEnemyInfo = enemyInfo;
            _enemyInfo = new EnemyInfo(enemyInfo);
        }

        // Временно чтобы заполнять поля через инспектор
        protected void Awake()
        {
            _enemyInfo = new EnemyInfo(_speed, _hp, _baseDmg, _enemyType);

            StartCoroutine(RotateAnim());
        }

        public event EventHandler death;

        public void TakeDMG(int dmg)
        {
            if (_enemyInfo.Hp - _enemyInfo.BaseDmg > 0) _enemyInfo.Hp -= _enemyInfo.BaseDmg;
            else Death();
        }

        public void Death()
        {
            death.Invoke(this, new EventArgs());
            Debug.Log("You win");
        }

        public virtual void AI()
        {
            gameObject.transform.position += Vector3.Normalize(Player.ins.transform.position - gameObject.transform.position) * _enemyInfo.Speed * Time.deltaTime;
        }

        public void Update()
        {
            AI();
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.ins.TakeDMG(_enemyInfo.BaseDmg);
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
