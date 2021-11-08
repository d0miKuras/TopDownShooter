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
    private GameObject _playerInstance;
    private TileAutomata tileAutomata;

    

    void Start()
    {
        tileAutomata = GetComponent<TileAutomata>();
    }

    public void SpawnPlayer()
    {
        Vector3 spawnPosition = tileAutomata.GetRandomCellPosition();

        _playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        _playerInstance.GetComponent<PlayerMovement>().cam = mainCam;
        cinemachineCamera.Follow = _playerInstance.transform;
    }

    public GameObject SpawnEnemy(GameObject enemyPrefab)
    {
        Vector3 spawnPosition = tileAutomata.GetRandomCellPosition();

        GameObject _enemyInstance = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        _enemyInstance.GetComponent<AIDestinationSetter>().target = _playerInstance.transform;
        return _enemyInstance;
    }
}
