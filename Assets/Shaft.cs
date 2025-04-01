using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaft : MonoBehaviour
{
    [Header("Scripts")]
    public Resources ResourcesScript;

    [Header("Stats")]
    public int metersDug;
    public float digPower, idlePower, wallDurability, mineProgress, DurabilityGain, bonusDrop;

    [Header("Power Dig")]
    public int powerDigs;
    public int maxPowerDigs;
    public float powerDigPower;

    [Header("Drops")]
    int dropsAmount;
    public int[] dropsWeigths;
    public int totalWeights;
    int roll, id;

    [Header("UI")]
    public RectTransform MineshaftForm;
    public Image ProgressBar;

    void Start()
    {
        Invoke("AutoDig", 0.5f);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (powerDigs > 0)
            {
                Dig(digPower * powerDigPower);
                if (powerDigs == maxPowerDigs)
                    Invoke("GatherPower", 12f);
                powerDigs--;
            }
            else Dig(digPower);
        }
    }

    void GatherPower()
    {
        powerDigs++;
        if (powerDigs < maxPowerDigs)
            Invoke("GatherPower", 12f);
    }

    void AutoDig()
    {
        Dig(digPower * idlePower);
        Invoke("AutoDig", 0.5f);
    }

    public void Dig(float amount)
    {
        mineProgress += amount;
        bonusDrop += amount;
        if (bonusDrop >= 100f + wallDurability * 0.1f)
        {
            ResourcesScript.GainResource(Drop());
            bonusDrop -= 100f + wallDurability * 0.1f;
        }
        if (mineProgress >= wallDurability)
            WallDug();
        ProgressBar.fillAmount = mineProgress / wallDurability;
    }

    void WallDug()
    {
        ResourcesDrop();

        metersDug++;
        MineshaftForm.sizeDelta = new Vector2(75, metersDug * 5);

        DropsChanges();

        bonusDrop += mineProgress - wallDurability;
        mineProgress = 0;
        wallDurability += DurabilityGain;
        DurabilityGain += (metersDug + 14) / 22;
    }

    void ResourcesDrop()
    {
        dropsAmount = Random.Range(6 + metersDug / 3, 8 + metersDug / 2);
        dropsAmount /= 3;
        for (int i = 0; i < dropsAmount; i++)
        {
            ResourcesScript.GainResource(Drop());
        }
    }

    int Drop()
    {
        roll = Random.Range(0, totalWeights);
        id = 0;
        while (roll >= dropsWeigths[id])
        {
            roll -= dropsWeigths[id];
            id++;
        }
        return id;
    }

    void DropsChanges()
    {
        if (metersDug % 3 == 0)
        {
            dropsWeigths[1] += 1 + metersDug / 30;
            totalWeights += 1 + metersDug / 30;
        }
        if (metersDug % 5 == 0)
        {
            dropsWeigths[2] += 1 + metersDug / 55;
            totalWeights += 1 + metersDug / 55;
            if (dropsWeigths[0] * metersDug >= 250)
            {
                dropsWeigths[0]--;
                dropsWeigths[1]++;
            }
        }
        if (metersDug % 9 == 0)
        {
            dropsWeigths[3] += 1 + metersDug / 108;
            totalWeights += 1 + metersDug / 108;
            if (dropsWeigths[1] * 2 + metersDug > 100)
            {
                dropsWeigths[1]--;
                dropsWeigths[2]++;
            }
        }
        if (metersDug % 13 == 0)
        {
            dropsWeigths[4] += 1 + metersDug / 169;
            totalWeights += 1 + metersDug / 169;
            if (dropsWeigths[2] * 3 + metersDug > 300)
            {
                dropsWeigths[2]--;
                dropsWeigths[3]++;
            }
        }
    }
}
