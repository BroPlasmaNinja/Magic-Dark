using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.RunScripts.ScriptableObjects;

public sealed class RunController : MonoBehaviour
{
    uint Souls;
    readonly uint[] LevelUpBorders;
    uint timer;

    List<Wave> AvailableWaves;

    void OnEnd()
    {

    }
    void OnPlayerDeath()
    {

    }
    float difficultyCoef;
    IEnumerator Timer()
    {
        while (true)
        {
            timer += 1;
            Debug.Log(timer);
            yield return new WaitForSeconds(1);
        }
    }
    private void Awake()
    {
        GameManager.runController = this;
        StartCoroutine(Timer());
    }
}