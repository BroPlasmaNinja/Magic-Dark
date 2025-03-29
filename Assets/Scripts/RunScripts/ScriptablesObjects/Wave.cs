using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.RunScripts.Interfaces;
using Assets.Scripts.RunScripts.Enums;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Wave", menuName = "InfoPreFabs/Wave")]
    internal class Wave : ScriptableObject, IAvailableWithProgress
    {
        //Два листа соединяются в кортеж по индексу
        [SerializeField]
        public List<EnemyInfo> EnemiesList;
        [SerializeField]
        public List<int> EnemiesCount;

        //Два листа соединяются в кортеж по индексу
        [SerializeField]
        public List<uint> countRepeat;
        [SerializeField]
        public List<uint> waitTimeMs;

        //Минимальный таймер спавна
        [SerializeField]
        public uint MinTimer;
        //Максимальный таймер спавна
        [SerializeField]
        public uint MaxTimer;

        [SerializeField]
        private ulong _necessarySouls;
        [SerializeField]
        public WaveType Type;
        public ulong necessarySouls => _necessarySouls;
    }
}
