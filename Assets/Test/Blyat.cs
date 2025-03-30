using Assets.Scripts.RunScripts.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.RunScripts;

public class Blyat : MonoBehaviour
{
    [SerializeField]
    EnemyInfo enemyInfo;
    private void Start()
    {
        var en = new Enemy(new EnemyInfo(enemyInfo)).CreateObject(null);
        en.transform.position = transform.position;
    }
}
