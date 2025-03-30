using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.RunScripts.ScriptableObjects;
using System;
using System.Linq;
using Assets.Scripts.RunScripts.Enums;
using Assets.Scripts.RunScripts;

public sealed class RunController : MonoBehaviour
{
    public uint Souls { set
        {
            _souls = value;
        }
        get
        {
            return _souls;
        }
    }
    private uint _souls = 0;
    [SerializeField]
    private uint[] LevelUpBorders;
    Queue<uint> _levelUpBordersQueue = new Queue<uint>();
    uint timer = 1;
    event EventHandler nextWave;
    private Queue<Wave> waves = new Queue<Wave>();
    [SerializeField]
    private uint StrongWaveTime;
    [SerializeField]
    private uint TimeBetweenWaves;
    [SerializeField]
    List<Wave> AvailableWaves;
    public float spawnRadius = 10f; // Радиус, в котором будут спавниться враги
    public float minSpawnRadius = 5f; // Минимальный радиус, чтобы враги не спавнились слишком близко к игроку
    public event EventHandler LVLUP;


    private void Start()
    {
        LVLUP.Invoke(this, new EventArgs());

    }

    private void LvlChecking()
    {
        if (_levelUpBordersQueue.First() < _souls)
        {
            LVLUP.Invoke(this, new EventArgs());
        }
    }

    private void StartSpawnWave(object sender, EventArgs args)
    {
        StartCoroutine(SpawnerWaves(waves.Peek()));
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
        for (int i = 0; i < currentWave.countRepeat.Count; i++)
        {
            for (int j = 0; j < currentWave.EnemiesCount[i]; j++)
            {
                for (int k = 0; k < currentWave.countRepeat[i]; k++)
                {
                    // Создание врага в случайной точке в радиусе от игрока
                    Vector3 spawnPosition = GetRandomSpawnPosition();
                    foreach (var enemy in currentWave.EnemiesList)
                    {
                        new Enemy(enemy).CreateObject(null).transform.position = spawnPosition;
                    }

                }
                yield return new WaitForSeconds(currentWave.waitTimeMs[i]);
            }
        }
        yield break;
    }

    // Метод для получения случайной позиции спавна в радиусе от игрока
    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool validPositionFound = false;

        while (!validPositionFound)
        {
            float randomAngle = UnityEngine.Random.Range(0f, 360f);
            float randomDistance = UnityEngine.Random.Range(minSpawnRadius, spawnRadius);
            spawnPosition = Player.ins.transform.position + Quaternion.Euler(0, randomAngle, 0) * Vector3.forward * randomDistance;

            if (Vector3.Distance(Player.ins.transform.position, spawnPosition) >= minSpawnRadius && Vector3.Distance(Player.ins.transform.position, spawnPosition) <= spawnRadius)
            {
                validPositionFound = true;
            }
        }

        return spawnPosition;
    }

    private void Awake()
    {
        GameManager.runController = this;
        foreach (var item in LevelUpBorders)
        {
            _levelUpBordersQueue.Enqueue(item);
        }
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
        timer = 1;
    }
}