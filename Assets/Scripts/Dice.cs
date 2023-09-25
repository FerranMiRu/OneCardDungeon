using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Dice : MonoBehaviour
{
    private int diceValue;
    private TMP_Dropdown diceDropdownComponent;
    private TextMeshProUGUI diceTextComponent;

    public GameObject diceText;
    public GameObject diceSelect;


    public string GetSelectedStat()
    {
        return diceDropdownComponent.options[
            diceDropdownComponent.value
        ].text;
    }

    public int GetValue()
    {
        return diceValue;
    }

    public void Roll()
    {
        diceValue = Random.Range(1, 7);
        diceTextComponent.text = diceValue.ToString();
    }

    void Awake()
    {
        diceDropdownComponent = diceSelect.GetComponent<TMP_Dropdown>();
        diceTextComponent = diceText.GetComponent<TextMeshProUGUI>();
    }
}
