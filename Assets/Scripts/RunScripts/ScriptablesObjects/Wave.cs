using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.Enums;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Wave", menuName = "InfoPreFabs/Wave")]
    internal class Wave : ScriptableObject, IAvailableWithProgress
    {
        [SerializeField] 
        public List<(EnemyInfo, int Count)> Enemies;
        public List<(uint countRepeat, uint waitTimeMs)> InWaveInterval;
        [SerializeField]
        public uint MinTimer;
        [SerializeField]
        public uint MaxTimer;
        [SerializeField]
        private ulong _necessarySouls;
        [SerializeField]
        public WaveType Type;
        public ulong necessarySouls => _necessarySouls;
    }
}
