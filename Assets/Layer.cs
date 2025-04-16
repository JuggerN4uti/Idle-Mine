using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public Shaft ShaftScipt;
    public Resources ResourcesScript;
    public Block[] BlockInLayer;
    public int[] BlockID;
    public int currentTarget;

    void Start()
    {
        SetBlocks();
    }

    public void Dig(float amount)
    {
        BlockInLayer[currentTarget].Dig(amount);
    }

    public void BlockDug()
    {
        currentTarget++;
        if (currentTarget >= 36)
        {
            ShaftScipt.currentLayer++;
            Destroy(gameObject);
        }
    }

    void SetBlocks()
    {
        for (int i = 0; i < BlockInLayer.Length; i++)
        {
            BlockInLayer[i].SetBlock(BlockID[i]);
        }
    }
}
