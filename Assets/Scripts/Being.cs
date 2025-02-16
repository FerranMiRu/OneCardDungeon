using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Being : MonoBehaviour
{ 
    protected bool isDead = false;
    protected int currentRow = 0;
    protected int currentColumn = 0;
    protected Dictionary<string, int> turnStats = new();
    protected TextMeshProUGUI beingHealthTextComponent;

    public int startingRow;
    public int startingColumn;
    public int speed;
    public int attack;
    public int defense;
    public int reach;
    public int hp;
    public GameObject beingHealthText;


    protected abstract void Die();

    protected virtual void Start()
    {
        beingHealthTextComponent = beingHealthText.GetComponent<TextMeshProUGUI>();
        beingHealthTextComponent.text = hp.ToString();
    }

    public bool IsDead()
    {
        return isDead;
    }

    public virtual void MoveTo(int row, int column, Vector3 position, bool setStartingPosition = false)
    {
        if (!setStartingPosition)
        {
            Board.instance.UnoccupyTile(currentRow, currentColumn);

            if (currentRow == row || currentColumn == column)
            {
                turnStats["Speed"] -= 2;
            }
            else
            {
                turnStats["Speed"] -= 3;
            }
        }

        currentRow = row;
        currentColumn = column;

        transform.position = position;
    }

    public void SetStartingPosition()
    {
        Board.instance.SetBeingToStartingPosition(this, startingRow, startingColumn);
    }

    public virtual void TakeDamage(int damage)
    {
        hp -= damage;
        beingHealthTextComponent.text = hp.ToString();

        if (hp <= 0)
        {
            Die();
        }
    }
}
