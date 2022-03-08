using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    enemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    WaveConfigSO repeatWave;
    List<Transform> wavePoints;
    int wayPointIndex = 0;
    
    void Awake() 
    {
        enemySpawner = FindObjectOfType<enemySpawner>();    
    }
    
    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        wavePoints = waveConfig.GetWayPoints();
        transform.position = wavePoints[wayPointIndex].position;
        repeatWave = enemySpawner.GetRepeatWave();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }

    void FollowPath()
    {
        if (wayPointIndex < wavePoints.Count)
        {
            Vector3 targetPosition = wavePoints[wayPointIndex].position;
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,targetPosition,delta);
            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        
        else
        {
            Destroy(gameObject);
        }
    }
}
