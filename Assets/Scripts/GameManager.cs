using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int playerBaseSpeed = 1;
    public int playerBaseAttack = 1;
    public int playerBaseDefense = 1;
    public int playerBaseReach = 2;
    public int playerBaseHp = 6;
    public List<int> selectedValuesList;
    public Dictionary<string, int> playerTurnStats = new Dictionary<string, int>();
    public BoardManager boardScript;
    public static GameManager instance = null;

    private int level = 1;
    private Button energyPhaseDoneButton;
    private GameObject energySelector;
    private GameObject dice1;
    private GameObject dice1Selector;
    private GameObject dice2;
    private GameObject dice2Selector;
    private GameObject dice3;
    private GameObject dice3Selector;
    private GameObject warningText;
    private Player player;


    private void HideWarning()
    {
        warningText.GetComponent<TextMeshProUGUI>().text = "";
    }

    private void EnergyPhase(bool endEnergyPhase)
    {
        if (!endEnergyPhase)
        {
            dice1.GetComponent<TextMeshProUGUI>().text = Random.Range(1, 7).ToString();
            dice2.GetComponent<TextMeshProUGUI>().text = Random.Range(1, 7).ToString();
            dice3.GetComponent<TextMeshProUGUI>().text = Random.Range(1, 7).ToString();

            energySelector.SetActive(true);
        }
        else
        {
            playerTurnStats.Add(
                dice1Selector.GetComponent<TMP_Dropdown>().options[selectedValuesList[0]].text,
                int.Parse(dice1.GetComponent<TextMeshProUGUI>().text)
            );
            playerTurnStats.Add(
                dice2Selector.GetComponent<TMP_Dropdown>().options[selectedValuesList[1]].text,
                int.Parse(dice2.GetComponent<TextMeshProUGUI>().text)
            );
            playerTurnStats.Add(
                dice3Selector.GetComponent<TMP_Dropdown>().options[selectedValuesList[2]].text,
                int.Parse(dice3.GetComponent<TextMeshProUGUI>().text)
            );

            energySelector.SetActive(false);
            print("Speed " + playerTurnStats["Speed"]);
            print("Attack " + playerTurnStats["Attack"]);
            print("Defense " + playerTurnStats["Defense"]);
        }
    }

    private void Turn()
    {
        EnergyPhase(false);
    }

    void InitGame()
    {
        boardScript.SetupScene(level);
        Turn();
    }

    void Awake()
    {
        dice1 = GameObject.Find("Dice1");
        dice2 = GameObject.Find("Dice2");
        dice3 = GameObject.Find("Dice3");

        dice1Selector = GameObject.Find("SelectDice1");
        dice2Selector = GameObject.Find("SelectDice2");
        dice3Selector = GameObject.Find("SelectDice3");

        foreach (Button button in Button.FindObjectsOfType(typeof(Button))){
            if (button.name == "DoneButton")
                energyPhaseDoneButton = button;
        }
        energyPhaseDoneButton.onClick.AddListener(EndEnergyPhase);

        energySelector = GameObject.Find("EnergySelector");
        energySelector.SetActive(false);


        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();

        InitGame();
    }

    public void AddPlayer(Player script)
    {
        player = script;
    }

    public void EndEnergyPhase()
    {
        HashSet<int> selectedValues = new HashSet<int> {
            dice1Selector.GetComponent<TMP_Dropdown>().value,
            dice2Selector.GetComponent<TMP_Dropdown>().value,
            dice3Selector.GetComponent<TMP_Dropdown>().value
        };

        if (selectedValues.Count == 3)
        {
            selectedValuesList = new List<int>(selectedValues);
            EnergyPhase(true);
        }
        else
        {
            warningText = GameObject.Find("WarningText");
            warningText.GetComponent<TextMeshProUGUI>().text = "You must select a different attribute for each number!";
            Invoke("HideWarning", 2f);
        }
    }
}
