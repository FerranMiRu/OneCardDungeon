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


    void Start()
    {
        GameManager.instance.AddPlayer(this);
        hp = GameManager.instance.playerBaseHp;
    }
}
