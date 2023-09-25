using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoneButton : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI warningTextComponent;

    public GameObject warningText;


    private void HideWarning()
    {
        warningTextComponent.text = "";
        warningText.SetActive(false);
    }

    private void DoneEnergyPhase()
    {
        if (Dices.instance.CheckUniqueStats())
        {
            EnergySelector.instance.EndEnergyPhase();
        }
        else
        {
            warningText.SetActive(true);
            warningTextComponent.text = "You must select a different attribute for each number!";
            Invoke(nameof(HideWarning), 2f);
        }
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(DoneEnergyPhase);

        warningTextComponent = warningText.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        HideWarning();
    }
}
