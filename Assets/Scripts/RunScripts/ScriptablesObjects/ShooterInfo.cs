using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ShooterInfo", menuName = "InfoPreFabs/ShooterInfo")]
    public class ShooterInfo : EnemyInfo
    {
        public int Radiuse { get; protected set; }

        public ShooterInfo(EnemyInfo enemyInfo, ushort radiuse) : base(enemyInfo)
        {
            Radiuse = radiuse;
        }

        public ShooterInfo(float speed, int hp, int baseDmg, ushort radiuse) : base(speed, hp, baseDmg)
        {
            Radiuse = radiuse;
        }
    }
}
