using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        // это временно для тестов (начало)
        // Или оставить чтобы балансить в будущем? Или по другой какой причеине может.
        [SerializeField]
        protected float _speed;

        [SerializeField]
        protected int _hp;

        [SerializeField]
        protected int _baseDmg;
        // это временно для тестов (конец)

        protected EnemyInfo _enemyInfo;

        // Неизменяемый EnemyInfo
        public EnemyInfo _baseEnemyInfo { get; private set; }

        public Enemy(EnemyInfo enemyInfo)
        {
            _baseEnemyInfo = enemyInfo;
            _enemyInfo = new EnemyInfo(enemyInfo);
        }

        // Временно чтобы заполнять поля через инспектор
        protected void Awake()
        {
            _enemyInfo = new EnemyInfo(_speed, _hp, _baseDmg);
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
    }
}
