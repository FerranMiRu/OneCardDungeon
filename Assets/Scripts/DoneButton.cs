using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoneButton : MonoBehaviour
{
    public GameObject warningText;

    private Button button;
    private TextMeshProUGUI warningTextComponent;


    private void HideWarning()
    {
        warningTextComponent.text = "";
    }

    private void DoneEnergyPhase()
    {
        if (Dices.instance.CheckUniqueStats())
        {
            EnergySelector.instance.EndEnergyPhase();
        }
        else
        {
            warningTextComponent.text = "You must select a different attribute for each number!";
            Invoke("HideWarning", 2f);
        }
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(DoneEnergyPhase);

        warningTextComponent = warningText.GetComponent<TextMeshProUGUI>();
    }
}
