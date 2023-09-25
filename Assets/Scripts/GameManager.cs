using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string nextPhase = "Energy";

    public static GameManager instance = null;


    private void InitGame()
    {
        NextPhase();
    }

    public void NextPhase()
    {
        if (nextPhase == "Energy")
        {
            nextPhase = "Player";
            EnergySelector.instance.StartEnergyPhase();
        }
        else if (nextPhase == "Player")
        {
            nextPhase = "Enemies";
            Player.instance.StartPlayerPhase();
        }
        else if (nextPhase == "Enemies")
        {
            nextPhase = "Energy";
            Enemies.instance.StartEnemiesPhase();
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitGame();
    }
}
