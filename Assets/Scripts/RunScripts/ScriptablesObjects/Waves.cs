using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.RunScripts.Interfaces;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    internal class Waves : ScriptableObject, IAvailableWithProgress
    {
        [SerializeField] 
        List<(EnemyInfo, int Count)> Enemies;
        [SerializeField] 
        uint MinTimer;
        [SerializeField] 
        uint MaxTimer;
        [SerializeField]
        public ulong _necessarySouls;
        public ulong necessarySouls => _necessarySouls;
    }
}
