using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Pathfinding;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera cinemachineCamera;
    public Camera mainCam;

    public HealthBar healthBar;
    private GameObject _playerInstance;
    private TileAutomata tileAutomata;

    void Start()
    {
        tileAutomata = GetComponent<TileAutomata>();
        // healthBar = GetComponent<HealthBar>();
    }

    public GameObject SpawnPlayer()
    {
        Vector3 spawnPosition = tileAutomata.GetRandomCellPosition();

        _playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        _playerInstance.GetComponent<PlayerMovement>().cam = mainCam;
        _playerInstance.GetComponent<HealthComponent>().healthBar = healthBar;
        cinemachineCamera.Follow = _playerInstance.transform;
        return _playerInstance;
    }

    public GameObject SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = tileAutomata.GetRandomCellPosition();

        GameObject _enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        _enemyInstance.GetComponent<AIDestinationSetter>().target = _playerInstance.transform;

        var damageComponent = _enemyInstance.GetComponent<AIDamageComponent>();
        damageComponent.playerTransform = _playerInstance.transform;

        return _enemyInstance;
    }
}
