using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer : MonoBehaviour
{
    public Shaft ShaftScipt;
    public Resources ResourcesScript;
    public Block[] BlockInLayer;
    public int currentTarget;

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
}
