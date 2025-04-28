using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tavern : MonoBehaviour
{
    [Header("Scripts")]
    public Miners MinersScript;

    [Header("Stats")]
    public float diggingSpeedMultiplyer;
    public float beer;
    public float maxBeer, beerPerClick;
    public Image BeerFill;

    void Update()
    {
        if (beer > 0f)
            beer -= Time.deltaTime;

        BeerFill.fillAmount = beer / maxBeer;
    }

    public void Refill()//float amount)
    {
        beer += beerPerClick;
        if (beer > maxBeer)
            beer = maxBeer;
        BeerFill.fillAmount = beer / maxBeer;
    }
}
