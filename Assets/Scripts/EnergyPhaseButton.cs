using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPhaseButton : MonoBehaviour
{
    void OnClick()
    {
        GameManager.instance.EndEnergyPhase();
    }
}
