using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Being
{
    private bool firstPlayerPhase = true;
    private Button skipPhaseButtonComponent;

    public GameObject skipPhaseButton;
    public static Player instance = null;


    private void DisplayPossibleActions()
    {
        Board.instance.DisplayPossibleMoves(currentRow, currentColumn, turnStats["Speed"]);
        Enemies.instance.DisplayPossibleAttacks(
            gameObject.transform.position, turnStats["Attack"], turnStats["Reach"]
        );
    }

    private void DisplayTotalStats()
    {
        PlayerStatsDisplay.instance.DisplayStats(turnStats);
    }

    protected override void Die()
    {
        Debug.Log("Player died");
    }

    protected override void Start()
    {
        skipPhaseButtonComponent = skipPhaseButton.GetComponent<Button>();
        skipPhaseButtonComponent.onClick.AddListener(SkipPhase);
        skipPhaseButtonComponent.interactable = false;

        base.Start();
    }

    public void AttackEnemy(int enemyDefense)
    {
        turnStats["Attack"] -= enemyDefense;
    }

    public void NextAction()
    {
        DisplayTotalStats();
        DisplayPossibleActions();
    }

    public void ResetStatsDisplay()
    {
        PlayerStatsDisplay.instance.DisplayStats(new Dictionary<string, int>{
            {"Speed", speed},
            {"Attack", attack},
            {"Defense", defense},
            {"Reach", reach}
        });
    }

    public void SetTurnStats(Dictionary<string, int> stats)
    {
        turnStats["Speed"] = speed + stats["Speed"];
        turnStats["Attack"] = attack + stats["Attack"];
        turnStats["Defense"] = defense + stats["Defense"];
        turnStats["Reach"] = reach;
    }

    public void StartPlayerPhase()
    {
        if (firstPlayerPhase)
        {
            Enemies.instance.SetStartingPositions();
            SetStartingPosition();
            firstPlayerPhase = false;
        }

        skipPhaseButtonComponent.interactable = true;
        NextAction();
    }

    public void SkipPhase()
    {
        Board.instance.ResetTiles();
        ResetStatsDisplay();
        skipPhaseButtonComponent.interactable = false;
        GameManager.instance.NextPhase();
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
