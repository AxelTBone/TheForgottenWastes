using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private enum SpawnState { Spawning, Resting, Amount}

    [System.Serializable]
    public class Wave
    {
        [SerializeField] internal string name;
        [SerializeField] internal Transform enemy;
        [SerializeField] internal int amountOfEnemies;
        [SerializeField] internal float spawnRate;
    }

    [SerializeField] private Wave[] waves;
    [SerializeField] private int nextWave = 0;
    [SerializeField] private float waveRest = 3f;
    [SerializeField] private float waveCountDown;
    [SerializeField] private float searchCount = 1f;
    [SerializeField] private Transform[] spawnPoints;
    private SpawnState state = SpawnState.Amount;

    private void Start()
    {
        waveCountDown = waveRest;
    }

    private void Update()
    {
        if(state == SpawnState.Resting)
        {
            if(EnemyAlive() == false)
            {
                // New wave
                WaveCompleted();
            }
            else
            {
                return;
            }
        }
        if(waveCountDown <= 0)
        {
            if(state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    private void WaveCompleted()
    {
        state = SpawnState.Amount;
        waveCountDown = waveRest;
        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
        }
        else
        {
            nextWave++;
        }
    }

    private bool EnemyAlive() // Checkes if enemies are alive
    {
        searchCount -= Time.deltaTime;
        if(searchCount <= 0f)
        {
            searchCount = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) // No enemies returns bool false, this triggers an if statement in void update which in turn starts a new wave
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        // Spawns in wave
        state = SpawnState.Spawning;
        for(int x = 0; x < _wave.amountOfEnemies; x++)
        {
            Spawning(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }
        // Waits for wave
        state = SpawnState.Resting;
        yield break;
    }

    private void Spawning(Transform _enemy)
    {
        Transform _spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)]; // Spawns enemies at a random pre-defined point
        Instantiate(_enemy, _spawnPoint.position, _spawnPoint.rotation);
    }
}