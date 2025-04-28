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
    public Image IconImage;
    public Button ThisButton;
    public bool maxxed;

    void Update()
    {
        if (!maxxed)
        {
            if (ResourcesScript.gold >= Cost[upgradesCount])
                ThisButton.interactable = true;
            else ThisButton.interactable = false;
        }
    }

    public void Buy()
    {
        ResourcesScript.SpendGold(Cost[upgradesCount]);
        upgradesCount++;
        if (upgradesCount == Cost.Length)
            Maxxed();
        else CostText.text = Cost[upgradesCount].ToString("0");
    }

    void Maxxed()
    {
        maxxed = true;
        ThisButton.interactable = false;
        CostText.text = "";
        IconImage.enabled = false;
    }
}
