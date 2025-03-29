using Assets.Scripts.RunScripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyInfo", menuName = "InfoPreFabs/EnemyInfo")]
    public class EnemyInfo : ScriptableObject, IAvailableWithProgress
    {
        ulong _necessarySouls;
        
        public ulong necessarySouls => _necessarySouls;

        public Animation Animation { get; private set; }
        
        public float Speed { get; private set; }

        public int BaseDmg { get; private set; }

        public int Hp { get; set; }

        public EnemyInfo(EnemyInfo enemyInfo)
        {
            Speed = enemyInfo.Speed;
            Hp = enemyInfo.Hp;
            BaseDmg = enemyInfo.BaseDmg;
            Animation = enemyInfo.Animation;
        }
        public EnemyInfo(float speed, int hp, int baseDmg, Animation animation)
        {
            Speed += speed;
            Hp = hp;
            BaseDmg = baseDmg;
            Animation = animation;
        }
    }
}
