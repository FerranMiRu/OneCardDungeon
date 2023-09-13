using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    public int attack;
    public int defense;
    public int reach;
    public int hp;
    public static Player instance = null;

    private int turnSpeed;
    private int turnAttack;
    private int turnDefense;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void SetTurnStats(Dictionary<string, int> stats)
    {
        turnSpeed = stats["Speed"];
        turnAttack = stats["Attack"];
        turnDefense = stats["Defense"];

        print("Speed: " + turnSpeed);
        print("Attack: " + turnAttack);
        print("Defense: " + turnDefense);
    }
}
