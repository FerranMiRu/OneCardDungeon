using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
                Board.instance.CheckLineOfSight( attackerPosition, enemy.gameObject.transform.position))
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
