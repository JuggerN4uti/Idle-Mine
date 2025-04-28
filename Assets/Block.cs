using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public BlockLibrary BLib;
    public TextSpawner TextSpawnerScript;
    public SpriteRenderer ItsSprite;
    public Sprite[] StateSprites;
    public Layer ItsLayer;
    int damagedStages;

    [Header("stats")]
    public float toughness;
    public float max, eggChance;
    public int dropsList, goldMin, goldMax;
    public int[] resourceID, dropMin, dropMax;

    public void Dig(float amount)
    {
        toughness -= amount;
        TextSpawnerScript.PopUpText(amount);
        if (toughness <= 0f)
            Dug();
        else ItsSprite.sprite = StateSprites[DamagedSprite()];
    }

    void Dug()
    {
        ItsLayer.BlockDug();
        Drops();
        Destroy(gameObject);
    }

    void Drops()
    {
        ItsLayer.ResourcesScript.GainGold(Random.Range(goldMin, goldMax + 1));
        ItsLayer.ResourcesScript.EggDrop(eggChance);
        for (int i = 0; i < dropsList; i++)
        {
            ItsLayer.ResourcesScript.GainResource(resourceID[i], Random.Range(dropMin[i], dropMax[i] + 1));
        }
    }

    public void SetBlock(int id)
    {
        ItsSprite.sprite = BLib.BlocksList[id].StateSprites[0];
        toughness = BLib.BlocksList[id].toughness;
        max = toughness;
        dropsList = BLib.BlocksList[id].dropsList;
        goldMin = BLib.BlocksList[id].goldMin;
        goldMax = BLib.BlocksList[id].goldMax;

        for (int i = 0; i < 4; i++)
        {
            StateSprites[i] = BLib.BlocksList[id].StateSprites[i];
        }

        for (int i = 0; i < dropsList; i++)
        {
            resourceID[i] = BLib.BlocksList[id].resourceID[i];
            dropMin[i] = BLib.BlocksList[id].dropMin[i];
            dropMax[i] = BLib.BlocksList[id].dropMax[i];
        }
    }

    int DamagedSprite()
    {
        float temp = 1f - (toughness / max);
        temp *= StateSprites.Length;
        return Mathf.FloorToInt(temp);
    }
}
