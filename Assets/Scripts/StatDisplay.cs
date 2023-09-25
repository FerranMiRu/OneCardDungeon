using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatDisplay : MonoBehaviour
{
    private TextMeshProUGUI statValueTextComponent;

    public GameObject statValue;


    public void SetValue(int value)
    {
        statValueTextComponent.text = value.ToString();
    }

    void Awake()
    {
        statValueTextComponent = statValue.GetComponent<TextMeshProUGUI>();
    }
}
