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
        public float waitBetweenProjectileMs;
        [SerializeField]
        public float dmgcoef = 1;
        [SerializeField]
        public float speedcoef = 1;
        [SerializeField]
        public float cooldowncoef = 1;
        [SerializeField]
        public float countProjectilecoed = 1;
        [SerializeField]
        public float spreadAnglecoef = 1;
        [SerializeField]
        public float waitBetweenProjectileMscoef = 1;
        [SerializeField]
        public byte lvl;

        public void SetState(SpellInfo info)
        {
            sprite = info.sprite;
            dmg = info.dmg;
            speed = info.speed;
            cooldown = info.cooldown;
            countProjectiles = info.countProjectiles;
            spreadAnglecoef = info.spreadAnglecoef;
            waitBetweenProjectileMs = info.waitBetweenProjectileMs;
            lvl = info.lvl;
            dmgcoef = info.dmgcoef;
            speedcoef = info.speedcoef;
            cooldowncoef = info.cooldowncoef;
            countProjectilecoed += info.countProjectilecoed;
            spreadAnglecoef += info.spreadAnglecoef;
            waitBetweenProjectileMscoef += info.waitBetweenProjectileMscoef;
        }
    }
}
