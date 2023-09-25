using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dices : MonoBehaviour
{
    public List<GameObject> dicesList;
    public static Dices instance = null;


    public bool CheckUniqueStats() 
    {
        HashSet<string> stats = new HashSet<string>();

        dicesList.ForEach(
            dice => stats.Add(dice.GetComponent<Dice>().GetSelectedStat())
        );

        return stats.Count == 3;
    }

    public Dictionary<string, int> GetTurnStats()
    {
        Dictionary<string, int> stats = new Dictionary<string, int>();

        dicesList.ForEach(
            dice => stats.Add(
                dice.GetComponent<Dice>().GetSelectedStat(),
                dice.GetComponent<Dice>().GetValue()
            )
        );

        return stats;
    }

    public void RollEnergy()
    {
        dicesList.ForEach(dice => dice.GetComponent<Dice>().Roll());
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
}
