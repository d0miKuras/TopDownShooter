using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileAutomata : MonoBehaviour
{
    [Range(0, 100)]
    public int initChance;

    [Range(1, 8)]
    public int birthLimit;

    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numRepetitions;

    public int[,] terrainMap;
    public Vector3Int tmapSize;

    public Tilemap topMap;
    public Tilemap botMap;

    public Tile topTile;
    public Tile botTile;




    int width;
    int height;

    public void DoSim(int repNum)
    {
        ClearMap(false);
        width = tmapSize.x;
        height = tmapSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            InitPos();
        }

        for (int i = 0; i < numRepetitions; i++)
        {
            terrainMap = GenTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y] == 1)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / width, 0), topTile);
                }
                else
                {
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / width, 0), botTile);
                }
            }
        }
    }

    private int[,] GenTilePos(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighbors;

        BoundsInt bound = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighbors = 0;
                foreach (var b in bound.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighbors += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighbors++;
                    }
                }
                if (oldMap[x, y] == 1)
                {
                    if (neighbors < deathLimit) newMap[x, y] = 0;
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }
                if (oldMap[x, y] == 0)
                {
                    if (neighbors > birthLimit) newMap[x, y] = 1;
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }


        return newMap;
    }

    private void InitPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = UnityEngine.Random.Range(1, 101) < initChance ? 1 : 0;
            }
        }
    }

    private void ClearMap(bool complete)
    {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }

    public void GenerateMap()
    {
        DoSim(numRepetitions);
    }

    public Vector3 GetRandomCellPosition()
    {
        List<Vector3> availTiles = new List<Vector3>();
        Grid grid = botMap.layoutGrid;
        foreach (var position in botMap.cellBounds.allPositionsWithin)
        {
            if (!botMap.HasTile(position)) continue;

            availTiles.Add(grid.GetCellCenterWorld(position));
        }

        return availTiles[Random.Range(0, availTiles.Count)];
    }


    public List<Vector3> GetAvailableTiles()
    {
        List<Vector3> availTiles = new List<Vector3>();
        Grid grid = botMap.layoutGrid;
        foreach (var position in botMap.cellBounds.allPositionsWithin)
        {
            if (!botMap.HasTile(position)) continue;
            Debug.Log(grid.GetCellCenterWorld(position));
            availTiles.Add(grid.GetCellCenterWorld(position));
        }

        return availTiles;
    }
}
