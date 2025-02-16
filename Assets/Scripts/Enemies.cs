using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private List<Enemy> enemies = new();

    public List<GameObject> enemiesObjects;
    public static Enemies instance = null;


    public void CheckIfAllEnemiesAreDead()
    {
        foreach (Enemy enemy in enemies)
        {
            if (!enemy.IsDead())
            {
                return;
            }
        }

        Debug.Log("You won!");
    }

    public void DisplayPossibleAttacks(Vector3 attackerPosition, int attack, int reach)
    {
        foreach (Enemy enemy in enemies)
        {
            if (
                !enemy.IsDead()
                &&
                Board.instance.CheckLineOfSight(attackerPosition, enemy.gameObject.transform.position)
                &&
                Board.instance.CheckDistance(
                    Board.instance.GetTileFromVector(attackerPosition),
                    Board.instance.GetTileFromVector(enemy.gameObject.transform.position),
                    reach
                ).Item1
            )
            {
                if (attack >= enemy.defense)
                {
                    enemy.Display();
                }
            }
        };
    }

    public void SetStartingPositions()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.SetStartingPosition();
        }
    }

    public void StartEnemiesPhase()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.PerformTurn();
        }

        GameManager.instance.NextPhase();
    }

    public void ResetEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            enemy.Hide();
        }
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
        foreach (GameObject enemyObject in enemiesObjects)
        {
            enemies.Add(enemyObject.GetComponent<Enemy>());
        }
    }
}
