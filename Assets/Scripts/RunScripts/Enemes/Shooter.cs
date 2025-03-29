using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    public class Shooter : Enemy, ICanCast
    {
        [SerializeField]
        private ushort _radiuse;

        public Shooter(EnemyInfo enemyInfo, ushort radiuse) : base(enemyInfo)
        {
            _radiuse = radiuse;
        }

        public override void AI()
        {
            Debug.Log((Player.ins.transform.position-gameObject.transform.position).magnitude);
            if ((Player.ins.transform.position - gameObject.transform.position).magnitude > _radiuse) gameObject.transform.position += Vector3.Normalize(Player.ins.transform.position - gameObject.transform.position) * _enemyInfo.Speed * Time.deltaTime;
        }

        public void Cast()
        {
        }
    }
}
