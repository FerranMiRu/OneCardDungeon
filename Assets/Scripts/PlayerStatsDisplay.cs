using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    private Dictionary<string, GameObject> statDisplays = new Dictionary<string, GameObject>();

    public GameObject speedStatDisplay;
    public GameObject attackStatDisplay;
    public GameObject defenseStatDisplay;
    public GameObject reachStatDisplay;
    public static PlayerStatsDisplay instance = null;


    public void DisplayStats(Dictionary<string, int> stats)
    {
        if (statDisplays.Count != 4)
        {
            Start();
        }

        foreach (KeyValuePair<string, int> stat in stats)
        {
            statDisplays[stat.Key].GetComponent<StatDisplay>().SetValue(stat.Value);
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
        if (statDisplays.Count != 4)
        {
            statDisplays.Add("Speed", speedStatDisplay);
            statDisplays.Add("Attack", attackStatDisplay);
            statDisplays.Add("Defense", defenseStatDisplay);
            statDisplays.Add("Reach", reachStatDisplay);
        }
    }
}
