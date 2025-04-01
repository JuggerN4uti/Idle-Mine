using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miners : MonoBehaviour
{
    [Header("Scripts")]
    public Resources ResourcesScript;

    [Header("Miners")]
    public int[] WoodMiners;
    public int[] StoneMiners;

    void Start()
    {
        GetRandomWood();
    }

    public void GetRandomWood()
    {
        WoodMiners[Random.Range(0, WoodMiners.Length)]++;
    }

    public void GetRandomStone()
    {
        StoneMiners[Random.Range(0, StoneMiners.Length)]++;
    }

    public void OpenCloseMinersHUD()
    {

    }
}
