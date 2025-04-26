using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public Resources ResourcesScript;
    public int upgradesCount;
    public int[] Cost;
    public TMPro.TextMeshProUGUI CostText;
    public Button ThisButton;

    void Update()
    {
        if (ResourcesScript.gold >= Cost[upgradesCount])
            ThisButton.interactable = true;
        else ThisButton.interactable = false;
    }

    public void Buy()
    {
        ResourcesScript.SpendGold(Cost[upgradesCount]);
        upgradesCount++;
        CostText.text = Cost[upgradesCount].ToString("0");
    }
}
