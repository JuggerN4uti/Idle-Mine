using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersLibrary : MonoBehaviour
{
    public Miner[] Miners;
    public int[] WoodMinersID, StoneMinersID;
    int roll;

    public int RollWoodMiner()
    {
        roll = Random.Range(0, WoodMinersID.Length);
        return WoodMinersID[roll];
    }

    public int RollStoneMiner()
    {
        roll = Random.Range(0, StoneMinersID.Length);
        return StoneMinersID[roll];
    }
}
