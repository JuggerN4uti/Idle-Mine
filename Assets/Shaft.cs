using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shaft : MonoBehaviour
{
    [Header("Scripts")]
    public Drill DrillScript;
    public Resources ResourcesScript;
    public Miners MinersScript;
    public Layer[] LayersScript;
    public CameraControl CameraScript;

    [Header("Stats")]
    public int metersDug;
    public int currentLayer;
    public float digPower, idlePower, wallDurability, digMultiplyer, mineProgress, DurabilityGain, bonusDrop;

    [Header("Power Dig")]
    public int powerDigs;
    public int maxPowerDigs;
    public float powerDigPower, powerDigCooldown;
    public TMPro.TextMeshProUGUI PowerDigsText;

    [Header("Levels")]
    public int level;
    public GameObject[] LevelObject;
    public float[] levelProgress, levelDurability;
    public Image[] LevelProgressBar;

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
        //Invoke("AutoDig", 0.5f);
    }

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            if (powerDigs > 0)
            {
                Dig(digPower * powerDigPower);
                if (powerDigs == maxPowerDigs)
                    Invoke("GatherPower", powerDigCooldown);
                powerDigs--;
                PowerDigsText.text = powerDigs.ToString("");
            }
            else Dig(digPower);
        }*/
    }

    void GatherPower()
    {
        powerDigs++;
        PowerDigsText.text = powerDigs.ToString("");
        if (powerDigs < maxPowerDigs)
            Invoke("GatherPower", powerDigCooldown);
    }

    void AutoDig()
    {
        //Dig(digPower * idlePower);
        Dig(idlePower);
        Invoke("AutoDig", 0.5f);
    }

    public void Dig(float amount)
    {
        LayersScript[currentLayer].Dig(amount * digMultiplyer);

        /*mineProgress += amount;
        bonusDrop += amount;
        if (bonusDrop >= 100f + wallDurability * 0.1f)
        {
            ResourcesScript.GainResource(Drop());
            bonusDrop -= 100f + wallDurability * 0.1f;
        }
        if (mineProgress >= wallDurability)
            WallDug();
        ProgressBar.fillAmount = mineProgress / wallDurability;*/
    }

    public void DigLevel(float amount, int level)
    {
        levelProgress[level] += amount;
        if (levelProgress[level] >= levelDurability[level])
        {
            OreDug(level);
            levelProgress[level] -= levelDurability[level];
            levelDurability[level] += level * 1f + 1f;
        }
        LevelProgressBar[level].fillAmount = levelProgress[level] / levelDurability[level];
    }

    void WallDug()
    {
        ResourcesDrop();

        metersDug++;
        MineshaftForm.sizeDelta = new Vector2(75, metersDug * 5);
        if (metersDug % 25 == 0)
            UnlockLevel();

        DropsChanges();

        bonusDrop += mineProgress - wallDurability;
        mineProgress = 0;
        wallDurability += DurabilityGain;
        DurabilityGain += (metersDug + 14) / 22;
    }

    void OreDug(int level)
    {
        dropsAmount = Random.Range(3, 5 + level);
        for (int i = 0; i < dropsAmount; i++)
        {
            ResourcesScript.GainResource(OreDrop(level));
        }
    }

    void UnlockLevel()
    {
        LevelObject[level].SetActive(true);
        switch (level)
        {
            case 0:
                break;
        }
        level++;
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

    int OreDrop(int level)
    {
        id = level;
        for (int i = 0; i < 3 + level; i++)
        {
            if (Random.Range(0f, 100f + level * 1f) >= 60f)
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

    public void BuyClickUpgrade()
    {
        DrillScript.maxFuel += 10f;
        DrillScript.drillPower += 1.1f;
        //digPower += 1f;
    }

    public void BuyDigUpgrade()
    {
        digMultiplyer += 0.02f;
    }
}
