using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 5;
    public int rows = 5;
    public Count wallCount = new Count(2, 4);
    public GameObject floorTile;
    public GameObject wallTile;
    public GameObject enemyTile;
    public GameObject playerTile;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();


    void LoadLevel1()
    {
        Instantiate(enemyTile, new Vector3(3, 4, 0f), Quaternion.identity);
        Instantiate(enemyTile, new Vector3(4, 2, 0f), Quaternion.identity);
    }

    void LoadMapA()
    {
        Instantiate(wallTile, new Vector3(1, 1, 0f), Quaternion.identity);
        Instantiate(wallTile, new Vector3(3, 1, 0f), Quaternion.identity);
        Instantiate(wallTile, new Vector3(3, 3, 0f), Quaternion.identity);

        Instantiate(playerTile, new Vector3(0, 0, 0f), Quaternion.identity);
    }

    void LoadLevel(int level)
    {
        if (level == 1)
        {
            LoadMapA();
            LoadLevel1();
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject instance = Instantiate(
                    floorTile, new Vector3(x, y, 0f), Quaternion.identity
                ) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        LoadLevel(level);
    }
}
