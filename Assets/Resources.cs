using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    [Header("Scripts")]
    public Miners MinersScript;

    public int gold;
    public int[] resource, resourcesWorth;
    public TMPro.TextMeshProUGUI GoldText;
    public TMPro.TextMeshProUGUI[] Text;
    public Button[] BuyEggButton;

    void Start()
    {
        GainGold(0);
    }

    public void GainResource(int id, int amount = 1)
    {
        resource[id] += amount;
        Text[id].text = resource[id].ToString("");
    }

    public void SellResources(int id)
    {
        GainGold(resource[id] * resourcesWorth[id]);
        resource[id] = 0;
        Text[id].text = resource[id].ToString("");
    }

    public void GainGold(int amount)
    {
        gold += amount;
        GoldText.text = gold.ToString("");

        CheckEggs();
    }

    void SpendGold(int amount)
    {
        gold -= amount;
        GoldText.text = gold.ToString("");

        CheckEggs();
    }

    void CheckEggs()
    {
        if (gold >= 100)
            BuyEggButton[0].interactable = true;
        else BuyEggButton[0].interactable = false;

        if (gold >= 450)
            BuyEggButton[1].interactable = true;
        else BuyEggButton[1].interactable = false;
    }

    public void BuyWoodEgg()
    {
        SpendGold(100);
        MinersScript.GetRandomWood();
    }

    public void BuyStoneEgg()
    {
        SpendGold(450);
        MinersScript.GetRandomStone();
    }
}
