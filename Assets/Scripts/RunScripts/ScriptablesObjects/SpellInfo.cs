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
        [SerializeField]
        public AudioClip castSound;
        [SerializeField]
        public AudioClip bangSound;
        [SerializeField]
        private int _basedmg;
        [SerializeField]
        private float _basespeed;
        [SerializeField]
        private float _basecooldown;
        [SerializeField]
        private int _basecountProjectiles;
        [SerializeField]
        private float _basespreadAngle;
        [SerializeField]
        private float _basewaitBetweenProjectileMs;

        public int dmg => (int)(_basedmg*lvl*dmgcoef);
        public float speed => _basespeed*lvl*speedcoef;
        public float cooldown => _basecooldown*lvl*cooldowncoef;
        public int countProjectiles => (int)(_basecountProjectiles * lvl * countProjectilecoef);
        public float spreadAngle => _basespreadAngle * lvl * spreadAnglecoef;
        public float waitBetweenProjectileMs => _basewaitBetweenProjectileMs * lvl * waitBetweenProjectileMscoef;
        [SerializeField]
        public float dmgcoef = 1;
        [SerializeField]
        public float speedcoef = 1;
        [SerializeField]
        public float cooldowncoef = 1;
        [SerializeField]
        public float countProjectilecoef = 1;
        [SerializeField]
        public float spreadAnglecoef = 1;
        [SerializeField]
        public float waitBetweenProjectileMscoef = 1;
        [SerializeField]
        public byte lvl = 1;

        public void SetState(SpellInfo info)
        {
            sprite = info.sprite;
            _basedmg = info._basedmg;
            _basespeed = info._basespeed;
            _basecooldown = info._basecooldown;
            _basecountProjectiles = info._basecountProjectiles;
            _basespreadAngle = info.spreadAnglecoef;
            _basewaitBetweenProjectileMs = info._basewaitBetweenProjectileMs;
            castSound = info.castSound;
            bangSound = info.bangSound;
            lvl = info.lvl;
            dmgcoef = info.dmgcoef;
            speedcoef = info.speedcoef;
            cooldowncoef = info.cooldowncoef;
            countProjectilecoef += info.countProjectilecoef;
            spreadAnglecoef += info.spreadAnglecoef;
            waitBetweenProjectileMscoef += info.waitBetweenProjectileMscoef;
        }
    }
}
