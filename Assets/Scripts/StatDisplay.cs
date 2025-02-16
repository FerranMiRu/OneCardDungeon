using TMPro;
using UnityEngine;

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
