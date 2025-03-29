using Assets.Scripts.RunScripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ItemInfo", menuName = "InfoPreFabs/ItemInfo")]
    internal class ItemInfo : ScriptableObject, IAvailableWithProgress
    {
        ulong _necessarySouls;
        public ulong necessarySouls => _necessarySouls;

    }
}
