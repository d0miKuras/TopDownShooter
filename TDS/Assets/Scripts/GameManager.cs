using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UI;
using System;
public enum GameState
{
    Prewave,
    Wave,
    Postwave
};

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public TileAutomata tileAutomata;
    public AstarPath pathfinder;
    public float beetlePerWaveCoefficient = 2f;
    public float timeBetweenWaves = 3.0f;
    public Text waveCounter;
    GameState state = GameState.Prewave;
    
    
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
        SetCurrentWave();
        spawnManager = GetComponent<SpawnManager>();
        spawnManager.SpawnPlayer();
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if(frameCount < 2) // I know this is bad, scanning it in start didnt work...
        {
            AstarPath.active.Scan(AstarPath.active.data.gridGraph);
            frameCount++;
        }

        if(_enemiesAlive == 0 && state == GameState.Wave)
        {
            state = GameState.Postwave;
            Debug.Log("All enemies died.");
            StartCoroutine(StartNewWave());
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
        state = GameState.Wave;
    }

    IEnumerator StartNewWave()
    {
        _currentWave++;
        SetCurrentWave();
        state = GameState.Prewave;
        yield return new WaitForSeconds(timeBetweenWaves);
        SpawnEnemies();
    }

    public void EnemyDied()
    {
        _enemiesAlive--;
    }

    public void SetCurrentWave()
    {
        waveCounter.text = $"{_currentWave}";
    }
}
