using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int gold;
    public int[] resource, resourcesWorth;
    public TMPro.TextMeshProUGUI GoldText;
    public TMPro.TextMeshProUGUI[] Text;

    public void GainResource(int id)
    {
        resource[id]++;
        Text[id].text = resource[id].ToString("");
    }

    public void SellResources(int id)
    {
        GainGold(resource[id] * resourcesWorth[id]);
        resource[id] = 0;
        Text[id].text = resource[id].ToString("");
    }

    void GainGold(int amount)
    {
        gold += amount;
        GoldText.text = gold.ToString("");
    }
}
