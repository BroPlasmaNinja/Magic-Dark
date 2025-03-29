using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyInfo", menuName = "InfoPreFabs/EnemyInfo")]
    public class EnemyInfo : ScriptableObject
    {

        public float Speed { get; private set; }

        public int BaseDmg { get; private set; }

        public int Hp { get; set; }

        public EnemyInfo(EnemyInfo enemyInfo)
        {
            Speed = enemyInfo.Speed;
            Hp = enemyInfo.Hp;
            BaseDmg = enemyInfo.BaseDmg;
        }
        public EnemyInfo(float speed, int hp, int baseDmg)
        {
            Speed += speed;
            Hp = hp;
            BaseDmg = baseDmg;
        }
    }
}
