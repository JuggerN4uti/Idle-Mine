using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour
{
    [Header("Scripts")]
    public Miners MinersScript;

    public int gold, eggs;
    public float totalLuck, pityChance;
    public int[] resource, resourcesWorth;
    public TMPro.TextMeshProUGUI GoldText, EggsText;
    public TMPro.TextMeshProUGUI[] Text;
    public Button[] BuyEggButton;

    void Start()
    {
        GainGold(0);
        Invoke("Pity", 0.7f);
    }

    public void GainResource(int id, int amount = 1)
    {
        resource[id] += amount;
        Text[id].text = resource[id].ToString("");
    }

    public void SpendResources(int id, int amount)
    {
        //GainGold(resource[id] * resourcesWorth[id]);
        resource[id] -= amount;
        Text[id].text = resource[id].ToString("");
    }

    public void GainGold(int amount)
    {
        gold += amount;
        GoldText.text = gold.ToString("");

        CheckEggs();
    }

    public void SpendGold(int amount)
    {
        gold -= amount;
        GoldText.text = gold.ToString("");

        CheckEggs();
    }

    public void GainEgg(int amount = 1)
    {
        eggs += amount;
        EggsText.text = eggs.ToString("");

        CheckEggs();
    }

    void SpendEgg(int amount = 1)
    {
        eggs -= amount;
        EggsText.text = eggs.ToString("");

        CheckEggs();
    }

    void CheckEggs()
    {
        if (gold >= 250 || eggs > 0)
            BuyEggButton[0].interactable = true;
        else BuyEggButton[0].interactable = false;

        /*if (gold >= 450)
            BuyEggButton[1].interactable = true;
        else BuyEggButton[1].interactable = false;*/
    }

    public void BuyEgg()
    {
        if (eggs > 0)
            SpendEgg();
        else SpendGold(250);
        MinersScript.OpenEgg();
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

    public void EggDrop(float chance)
    {
        totalLuck = chance + pityChance;
        if (totalLuck >= Random.Range(0f, 500f + totalLuck))
        {
            GainEgg();
            pityChance = 0f;
        }
        else pityChance += chance * 0.1f;
    }

    void Pity()
    {
        pityChance += 0.07f;
        Invoke("Pity", 0.7f);
    }
}
