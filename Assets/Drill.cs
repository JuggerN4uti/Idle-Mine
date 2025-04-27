using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drill : MonoBehaviour
{
    [Header("Scripts")]
    public Shaft ShaftScript;

    [Header("Fuel")]
    public float fuel;
    public float maxFuel, fuelPerClick;
    public Image FuelFill;

    [Header("Drill Stats")]
    public float drillPower;

    void Start()
    {
        Invoke("Bore", 0.6f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Refuel(fuelPerClick);

        FuelFill.fillAmount = fuel / maxFuel;

        if (fuel > 0f)
            fuel -= Time.deltaTime;
    }

    void Refuel(float amount)
    {
        fuel += amount;
        if (fuel > maxFuel)
            fuel = maxFuel;
    }

    void Bore()
    {
        if (fuel > 0f)
            ShaftScript.Dig(drillPower);
        Invoke("Bore", 0.6f);
    }
}
