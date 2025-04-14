using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Layer ItsLayer;
    public float toughness;
    public int dropsList, goldMin, goldMax;
    public int[] resourceID, dropMin, dropMax;

    public void Dig(float amount)
    {
        toughness -= amount;
        if (toughness <= 0f)
            Dug();
    }

    void Dug()
    {
        ItsLayer.BlockDug();
        Drops();
        Destroy(gameObject);
    }

    void Drops()
    {
        ItsLayer.ResourcesScript.GainGold(Random.Range(goldMin, goldMax));
        for (int i = 0; i < dropsList; i++)
        {
            ItsLayer.ResourcesScript.GainResource(resourceID[i], Random.Range(dropMin[i], dropMin[i]));
        }
    }
}
