using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Tilemaps;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    public CinemachineVirtualCamera cinemachineCamera;
    public Camera mainCam;
    private GameObject _playerInstance;
    private TileAutomata tileAutomata;
    private List<Tile> availTiles;


    void Start()
    {
        tileAutomata = GetComponent<TileAutomata>();
        tileAutomata.GenerateMap();
        SpawnPlayer();
    }


    Vector3 GetRandomCellPosition()
    {
        List<Vector3> availTiles = new List<Vector3>();
        Grid grid = tileAutomata.botMap.layoutGrid;
        foreach(var position in tileAutomata.botMap.cellBounds.allPositionsWithin)
        {
            if(!tileAutomata.botMap.HasTile(position)) continue;

            availTiles.Add(grid.GetCellCenterWorld(position));
        }

        return availTiles[Random.Range(0, availTiles.Count)];
    }

    void SpawnPlayer()
    {
        Vector3 spawnPosition = GetRandomCellPosition();

        _playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        _playerInstance.GetComponent<PlayerMovement>().cam = mainCam;
        cinemachineCamera.Follow = _playerInstance.transform;
    }
}
