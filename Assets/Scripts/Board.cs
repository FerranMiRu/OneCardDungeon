using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    private readonly List<List<Tile>> boardMatrix = new();
    private class QueueObject
    {
        public Tile tile;
        public int distance;
        public QueueObject(Tile tile, int distance)
        {
            this.tile = tile;
            this.distance = distance;
        }
    }
    private class Queue
    {
        private readonly List<QueueObject> queue = new();
        private readonly List<QueueObject> visited = new();
        public void Enqueue(QueueObject queueObject)
        {
            foreach (QueueObject visitedObject in visited)
            {
                if (visitedObject.tile == queueObject.tile)
                {
                    return;
                }
            }

            foreach (QueueObject inQueueObject in queue)
            {
                if (inQueueObject.tile == queueObject.tile)
                {
                    return;
                }
            }

            queue.Add(queueObject);
        }
        public QueueObject Dequeue()
        {
            QueueObject queueObject = queue[0];
            queue.RemoveAt(0);
            visited.Add(queueObject);
            return queueObject;
        }
        public bool IsEmpty()
        {
            return queue.Count == 0;
        }
    }

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

    public (bool, int) CheckDistance(Tile origin, Tile target, int reach)
    {
        (bool, int) best = (false, 0);
        Queue queue = new();
        queue.Enqueue(new QueueObject(origin, 0));

        while (!queue.IsEmpty())
        {
            QueueObject current = queue.Dequeue();

            if (current.tile == target)
            {
                best = (true, current.distance);
            }

            if (reach - current.distance < 2)
            {
                continue;
            }

            if (current.tile.row < 4)
            {
                queue.Enqueue(new QueueObject(
                    boardMatrix[current.tile.row + 1][current.tile.column], current.distance + 2)
                );
            }
            if (current.tile.row > 0)
            {
                queue.Enqueue(new QueueObject(
                    boardMatrix[current.tile.row - 1][current.tile.column], current.distance + 2)
                );
            }
            if (current.tile.column < 4)
            {
                queue.Enqueue(new QueueObject(
                    boardMatrix[current.tile.row][current.tile.column + 1], current.distance + 2)
                );
            }
            if (current.tile.column > 0)
            {
                queue.Enqueue(new QueueObject(
                    boardMatrix[current.tile.row][current.tile.column - 1], current.distance + 2)
                );
            }

            if (reach - current.distance < 3)
            {
                continue;
            }

            if (current.tile.row < 4 && current.tile.column < 4)
            {
                queue.Enqueue(new QueueObject(
                    boardMatrix[current.tile.row + 1][current.tile.column + 1], current.distance + 3)
                );
            }
            if (current.tile.row > 0 && current.tile.column > 0)
            {
                queue.Enqueue(new QueueObject(
                    boardMatrix[current.tile.row - 1][current.tile.column - 1], current.distance + 3)
                );
            }
            if (current.tile.row < 4 && current.tile.column > 0)
            {
                queue.Enqueue(new QueueObject(
                    boardMatrix[current.tile.row + 1][current.tile.column - 1], current.distance + 3)
                );
            }
            if (current.tile.row > 0 && current.tile.column < 4)
            {
                queue.Enqueue(new QueueObject(
                    boardMatrix[current.tile.row - 1][current.tile.column + 1], current.distance + 3)
                );
            }
        }

        return best;
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

    public Tile GetTileFromVector(Vector3 vector)
    {
        foreach (List<Tile> row in boardMatrix)
        {
            foreach (Tile tile in row)
            {
                if (tile.gameObject.transform.position == vector)
                {
                    return tile;
                }
            }
        }

        return null;
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
