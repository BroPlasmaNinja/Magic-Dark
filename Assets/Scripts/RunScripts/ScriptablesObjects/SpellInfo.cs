using Assets.Scripts.RunScripts.Interfaces;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "SpellInfo", menuName = "InfoPreFabs/SpellInfo")]
    public class SpellInfo : ScriptableObject, IAvailableWithProgress
    {
        ulong _necessarySouls;
        public ulong necessarySouls => _necessarySouls;
        [SerializeField]
        public Sprite sprite;
        //Здесь оставь звук каста
        //Здесь оставь звук попадения
        [SerializeField]
        public int dmg;
        [SerializeField]
        public float speed;
        [SerializeField]
        public float cooldown;
        [SerializeField]
        public int countProjectiles;
        [SerializeField]
        public float spreadAngle;
        [SerializeField]
        public float dmgcoef;
        [SerializeField]
        public float speedcoef;
        [SerializeField]
        public float cooldowncoef;
        [SerializeField]
        public float countProjectilecoed;
        [SerializeField]
        public float spreadAnglecoef;
        [SerializeField]
        public byte lvl;

        public SpellInfo(SpellInfo info)
        {
            sprite = info.sprite;
            dmg = info.dmg;
            speed = info.speed;
            cooldown = info.cooldown;
            lvl = info.lvl;
            dmgcoef = info.dmgcoef;
            speedcoef = info.speedcoef;
            cooldowncoef = info.cooldowncoef;
        }
    }
}
