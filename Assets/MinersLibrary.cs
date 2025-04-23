using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersLibrary : MonoBehaviour
{
    public Miner[] Miners;
    public int[] WoodMinersID, StoneMinersID, IronMinersID, GoldMinersID, DiamondMinersID;
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

    public int RollIronMiner()
    {
        roll = Random.Range(0, IronMinersID.Length);
        return IronMinersID[roll];
    }

    public int RollGoldMiner()
    {
        roll = Random.Range(0, GoldMinersID.Length);
        return GoldMinersID[roll];
    }

    public int RollDiamondMiner()
    {
        roll = Random.Range(0, DiamondMinersID.Length);
        return DiamondMinersID[roll];
    }
}
