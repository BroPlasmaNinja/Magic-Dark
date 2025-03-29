using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public class Enemy : MonoBehaviour, IDamagable
    {
        // это временно для тестов (начало)
        // Или оставить чтобы балансить в будущем? Или по другой какой причеине может.
        [SerializeField]
        private float _speed = 0.5f;

        [SerializeField]
        private int _hp = 50;

        [SerializeField]
        private int _baseDmg = 50;
        // это временно для тестов (конец)

        private EnemyInfo _enemyInfo;

        // Неизменяемый EnemyInfo
        public EnemyInfo _baseEnemyInfo { get; private set; }

        public Enemy(EnemyInfo enemyInfo)
        {
            _baseEnemyInfo = enemyInfo;
            _enemyInfo = new EnemyInfo(enemyInfo);
        }

        // Временно чтобы заполнять поля через инспектор
        private void Awake()
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

        public void AI()
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
