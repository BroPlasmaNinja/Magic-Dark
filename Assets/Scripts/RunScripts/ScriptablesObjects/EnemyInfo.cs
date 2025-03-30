using Assets.Scripts.RunScripts.Enums;
using UnityEngine;

namespace Assets.Scripts.RunScripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyInfo", menuName = "InfoPreFabs/EnemyInfo")]
    public class EnemyInfo : ScriptableObject
    {

        [SerializeField]
        ulong _necessarySouls;

        public ulong necessarySouls => _necessarySouls;

        [SerializeField]
        public float speed;

        [SerializeField]
        public int baseDmg;

        [SerializeField]
        public int hp;

        [SerializeField]
        public EnemyType enemyType;

        [SerializeField]
        public Sprite sprite;

        [SerializeField]
        public Color color;

        public void SetState(EnemyInfo enemyInfo)
        {
            speed = enemyInfo.speed;
            hp = enemyInfo.hp;
            baseDmg = enemyInfo.baseDmg;
            sprite = enemyInfo.sprite;
            sprite = enemyInfo.sprite;
        }

        public EnemyInfo() { }

        public EnemyInfo(EnemyInfo enemyInfo)
        {
            speed = enemyInfo.speed;
            hp = enemyInfo.hp;
            baseDmg = enemyInfo.baseDmg;
            sprite = enemyInfo.sprite;
            sprite = enemyInfo.sprite;
        }
    }
}
