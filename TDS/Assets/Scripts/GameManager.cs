using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public TileAutomata tileAutomata;
    public AstarPath pathfinder;
    public float beetlePerWaveCoefficient = 2f;
    
    
    public GameObject beetlePrefab;
    private int _currentWave = 1;
    private int _enemiesAlive = 0;

    int frameCount = 0;

    void Awake()
    {
        tileAutomata = GetComponent<TileAutomata>();
        tileAutomata.GenerateMap();
        
    }
    void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
        spawnManager.SpawnPlayer();
        SpawnEnemies();

    }

    private IEnumerator ScanMap()
    {
        
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(frameCount < 2) // I know this is bad, scanning it in start didnt work...
        {
            AstarPath.active.Scan(AstarPath.active.data.gridGraph);
            frameCount++;
        }

        
    }

    void SpawnEnemies()
    {
        // Spawning beetles.
        for(int i = 0; i < Mathf.FloorToInt(_currentWave*beetlePerWaveCoefficient); i++)
        {
            var spawnedEnemy = spawnManager.SpawnEnemy(beetlePrefab);
            _enemiesAlive++;
        }
    }
}
