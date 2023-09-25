using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySelector : MonoBehaviour
{
    public static EnergySelector instance = null;


    public void StartEnergyPhase()
    {
        Player.instance.ResetStatsDisplay();
        Dices.instance.RollEnergy();

        gameObject.SetActive(true);
    }

    public void EndEnergyPhase()
    {
        gameObject.SetActive(false);

        Player.instance.SetTurnStats(Dices.instance.GetTurnStats());

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
