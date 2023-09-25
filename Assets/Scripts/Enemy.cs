using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Being
{
    private Button attackThisButtonComponent;

    public GameObject attackThisButton;


    private void AttackThis()
    {
        Player.instance.AttackEnemy(defense);
        TakeDamage(1);

        Enemies.instance.ResetEnemies();

        Player.instance.NextAction();
    }

    protected override void Die()
    {
        Board.instance.UnoccupyTile(currentRow, currentColumn);

        isDead = true;
        gameObject.SetActive(false);

        Enemies.instance.CheckIfAllEnemiesAreDead();
    }

    protected override void Start()
    {
        attackThisButtonComponent = attackThisButton.GetComponent<Button>();
        attackThisButtonComponent.onClick.AddListener(AttackThis);
        attackThisButtonComponent.interactable = false;

        base.Start();
    }

    public void Display()
    {
        attackThisButtonComponent.interactable = true;
    }

    public void Hide()
    {
        attackThisButtonComponent.interactable = false;
    }
}
