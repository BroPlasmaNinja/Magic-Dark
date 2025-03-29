﻿using Assets.Scripts.RunScripts.Interfaces;
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
        
        public float Speed { get; private set; }

        public int BaseDmg { get; private set; }

        public int Hp { get; set; }

        public EnemyType EnemyType { get; private set; }

        public EnemyInfo(EnemyInfo enemyInfo)
        {
            Speed = enemyInfo.Speed;
            Hp = enemyInfo.Hp;
            BaseDmg = enemyInfo.BaseDmg;
            EnemyType = enemyInfo.EnemyType;
        }
        public EnemyInfo(float speed, int hp, int baseDmg, EnemyType eTypy)
        {
            Speed += speed;
            Hp = hp;
            BaseDmg = baseDmg;
            EnemyType = eTypy;
        }
    }
}
