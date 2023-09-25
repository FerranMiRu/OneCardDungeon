using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Board : MonoBehaviour
{
    private readonly List<List<Tile>> boardMatrix = new();

    public float tileSize;
    public List<GameObject> tiles;
    public static Board instance = null;


    private void GenerateBoardMatrix()
    {
        int row = 0;
        int column = 0;

        foreach (GameObject tile in tiles)
        {
            if (boardMatrix.Count <= row)
            {
                boardMatrix.Add(new List<Tile>());
            }

            boardMatrix[row].Add(tile.GetComponent<Tile>());
            boardMatrix[row][column].SetCoordinates(row, column);

            column++;
            if (column == 5)
            {
                row++;
                column = 0;
            }
        }
    }

    public bool CheckLineOfSight(Vector3 origin, Vector3 target)
    {
        List<Vector3> originCorners = new();
        List<Vector3> targetCorners = new();

        originCorners.Add(new Vector3(origin.x - tileSize, origin.y - tileSize, 0));
        originCorners.Add(new Vector3(origin.x + tileSize, origin.y - tileSize, 0));
        originCorners.Add(new Vector3(origin.x - tileSize, origin.y + tileSize, 0));
        originCorners.Add(new Vector3(origin.x + tileSize, origin.y + tileSize, 0));

        targetCorners.Add(new Vector3(target.x - tileSize, target.y - tileSize, 0));
        targetCorners.Add(new Vector3(target.x + tileSize, target.y - tileSize, 0));
        targetCorners.Add(new Vector3(target.x - tileSize, target.y + tileSize, 0));
        targetCorners.Add(new Vector3(target.x + tileSize, target.y + tileSize, 0));

        foreach (Vector3 targetCorner in targetCorners)
        {
            foreach (Vector3 originCorner in originCorners)
            {
                if (Physics2D.Linecast(originCorner, targetCorner).collider == null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void DisplayPossibleMoves(int row, int column, int speed)
    {
        if (speed >= 2)
        {
            for (int i = row - 1; i <= row + 1; i++)
            {
                if (i < 0 || i > 4)
                    continue;

                for (int j = column - 1; j <= column + 1; j++)
                {
                    if (j < 0 || j > 4)
                        continue;

                    if (i == row && j == column)
                        continue;

                    if (i != row && j != column && speed < 3)
                        continue;

                    boardMatrix[i][j].Display();
                }
            }
        }
    }

    public void SetBeingToStartingPosition(Being being, int row, int column)
    {
        boardMatrix[row][column].MoveHere(being, true);
    }

    public void ResetTiles()
    {
        foreach (List<Tile> row in boardMatrix)
        {
            foreach (Tile tile in row)
            {
                tile.Hide();
            }
        }
    }

    public void UnoccupyTile(int row, int column)
    {
        boardMatrix[row][column].Unoccupy();
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        GenerateBoardMatrix();
    }
}
