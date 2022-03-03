using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    [SerializeField] bool islooping;
    WaveConfigSO currentWave;

    
    void Start()
    {                
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        do 
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(currentWave.GetEnemyPrefab(i),
                    currentWave.GetStartingWayPoint().position,
                    Quaternion.identity,
                    transform);
                    yield return new WaitForSecondsRealtime(currentWave.GetRandomSpawnTime());
                }    
                yield return new WaitForSecondsRealtime(timeBetweenWaves);
            }
        }
        while (islooping);
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }
}
