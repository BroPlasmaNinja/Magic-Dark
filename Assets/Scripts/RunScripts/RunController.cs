using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.RunScripts.ScriptableObjects;

public sealed class RunController : MonoBehaviour
{
    uint Souls;
    readonly uint[] LevelUpBorders;
    uint Timer;

    List<Wave> AvailableWaves;

    void OnEnd()
    {

    }
    void OnPlayerDeath()
    {

    }
    float difficultyCoef;
}