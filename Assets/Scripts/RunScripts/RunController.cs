using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using System.Linq;
using Assets.Scripts.RunScripts.Enums;

public sealed class RunController : MonoBehaviour
{
    uint Souls;
    readonly uint[] LevelUpBorders;
    uint timer = 1;
    event EventHandler nextWave;
    private Queue<Wave> waves;
    [SerializeField]
    private uint StrongWaveTime;

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
            if (timer % 15 == 0)
                nextWave.Invoke(this, new EventArgs());

            yield return new WaitForSeconds(1);
        }
    }
    private void Awake()
    {
        GameManager.runController = this;
        WavesPrepare();
        StartCoroutine(Timer());
    }
    private void WavesPrepare()
    {
        Wave[] buffer;
        Wave needed;
        while(timer<900)
        {
            if (timer%StrongWaveTime!=0) 
            {
                buffer = AvailableWaves.Where(x => x.MinTimer < timer && x.MaxTimer > timer).ToArray();
                needed = buffer[GameManager.rnd.Next(0, buffer.Count())];
            }
            else if(timer>=885)
            {
                buffer = AvailableWaves.Where(x => x.MinTimer < timer && x.MaxTimer > timer && x.Type==WaveType.Boss).ToArray();
                needed = buffer[GameManager.rnd.Next(0, buffer.Count())];
            }
            else
            {
                buffer = AvailableWaves.Where(x => x.MinTimer < timer && x.MaxTimer > timer && x.Type==WaveType.Strong).ToArray();
                needed = buffer[GameManager.rnd.Next(0, buffer.Count())];
            }
            waves.Enqueue(needed);
            timer += 15;
        }

    }
}