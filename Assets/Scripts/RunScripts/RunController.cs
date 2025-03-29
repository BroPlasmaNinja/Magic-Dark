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
    [SerializeField]
    private uint TimeBetweenWaves;
    [SerializeField]
    List<Wave> AvailableWaves;

    private void StartSpawnWave(object sender, EventArgs args)
    {

    }

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
    IEnumerator SpawnerWaves(Wave currentWave)
    {
        uint buffer = currentWave.InWaveInterval.Select(x=>x.countRepeat).Aggregate((x,y) => x+=y);
        for (int i = 0; i < currentWave.InWaveInterval.Count; i++)
        {
            for(int j = 0; j < currentWave.InWaveInterval[i].countRepeat; j++)
            {
                foreach(var enemy in currentWave.Enemies)
                {
                    for (int k = 0; k < enemy.Count; k++)
                    {

                    }
                }
                yield return new WaitForSeconds(1);
            }
        }
        yield break;
    }
    private void Awake()
    {
        GameManager.runController = this;
        WavesPrepare();
        nextWave += StartSpawnWave;
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