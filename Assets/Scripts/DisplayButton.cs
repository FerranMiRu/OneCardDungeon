using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayButton : MonoBehaviour
{
    public List<GameObject> itemsToToggle;
    public bool showEnergySelector = true;

    private Button button;
    private TextMeshProUGUI buttonTextComponent;


    private void OnToggle()
    {
        showEnergySelector = !showEnergySelector;
        itemsToToggle.ForEach(item => item.SetActive(showEnergySelector));
        buttonTextComponent.text = (
            showEnergySelector ? "Hide" : "Show"
        );
    }

    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnToggle);

        buttonTextComponent = button.GetComponentInChildren<TextMeshProUGUI>();
    }
}
