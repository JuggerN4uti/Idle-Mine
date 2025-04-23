using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Miners : MonoBehaviour
{
    [Header("Scripts")]
    public Shaft ShaftScript;
    public Resources ResourcesScript;
    public MinersLibrary MLib;

    [Header("Miners")]
    public int[] MinersInBase;
    public int[] MinersEquipped, MinersLocation;
    public bool[] SlotTaken;
    public TMPro.TextMeshProUGUI[] MinersLocationText;
    public int MinersMaxSlots, SlotsTaken;
    public Image[] EquippedMinerSprite;
    public Button[] EquippedMinerButton;
    public Sprite Chegg;

    [Header("Base HUD")]
    public GameObject MinersHUD;
    public GameObject[] BaseMinerObject;
    public Image[] BaseMinerSprite;
    public Button[] BaseMinerButton;
    public int[] BaseMinerID;
    int currentPlace;

    [Header("Luck")]
    public float luck;
    float totalLuck;

    void Start()
    {
        /*for (int i = 0; i < 1; i++)
        {
            GetRandomWood();
        }*/
        Invoke("AutoDig", 0.5f);
    }

    void AutoDig()
    {
        for (int i = 0; i < MinersEquipped.Length; i++)
        {
            if (SlotTaken[i])
                Dig(i);
        }
        Invoke("AutoDig", 0.5f);
    }

    void Dig(int miner)
    {
        if (MinersLocation[miner] == 0)
            ShaftScript.Dig(MLib.Miners[MinersEquipped[miner]].digPower);
        else
            ShaftScript.DigLevel(MLib.Miners[MinersEquipped[miner]].digPower, MinersLocation[miner] - 1);
    }

    public void OpenEgg()
    {
        if (LuckCheck(0f))
        {
            if (LuckCheck(0f))
            {
                if (LuckCheck(0f))
                {
                    if (LuckCheck(0f))
                    {
                        MinersInBase[MLib.RollLegendaryMiner()]++;
                    }
                    else MinersInBase[MLib.RollGoldMiner()]++;
                }
                else MinersInBase[MLib.RollIronMiner()]++;
            }
            else MinersInBase[MLib.RollStoneMiner()]++;
        }
        else MinersInBase[MLib.RollWoodMiner()]++;
    }

    public void GetRandomWood()
    {
        MinersInBase[MLib.RollWoodMiner()]++;
    }

    public void GetRandomStone()
    {
        MinersInBase[MLib.RollStoneMiner()]++;
    }

    public void OpenCloseMinersHUD()
    {
        if (MinersHUD.activeSelf)
            MinersHUD.SetActive(false);
        else
        {
            DisplayBase();
            MinersHUD.SetActive(true);
        }
    }

    void DisplayBase()
    {
        currentPlace = 0;
        for (int i = 0; i < MinersInBase.Length; i++)
        {
            if (MinersInBase[i] > 0)
            {
                for (int j = 0; j < MinersInBase[i]; j++)
                {
                    BaseMinerObject[currentPlace].SetActive(true);
                    BaseMinerSprite[currentPlace].sprite = MLib.Miners[i].MinerSprite;
                    BaseMinerID[currentPlace] = i;
                    currentPlace++;
                }
            }
        }
        for (int i = 0; i < currentPlace; i++)
        {
            if (SlotsTaken < MinersMaxSlots)
                BaseMinerButton[i].interactable = true;
            else BaseMinerButton[i].interactable = false;
        }

        for (int i = currentPlace; i < BaseMinerObject.Length; i++)
        {
            BaseMinerObject[i].SetActive(false);
        }
    }

    public void EquipMiner(int slot)
    {
        MinersInBase[BaseMinerID[slot]]--;
        int openSlot = ChoseSlot();
        MinersEquipped[openSlot] = BaseMinerID[slot];
        MinersLocation[openSlot] = 0;
        MinersLocationText[openSlot].text = "0";
        EquippedMinerSprite[openSlot].sprite = MLib.Miners[BaseMinerID[slot]].MinerSprite;
        EquippedMinerButton[openSlot].interactable = true;
        SlotTaken[openSlot] = true;
        SlotsTaken++;
        //ShaftScript.idlePower += MLib.Miners[BaseMinerID[slot]].digPower;
        DisplayBase();
    }

    int ChoseSlot()
    {
        int slot = 0;
        while (SlotTaken[slot])
        {
            slot++;
        }
        return slot;
    }

    public void SelectMiner(int slot)
    {
        if (MinersHUD.activeSelf)
            UnequipMiner(slot);
        else if (ShaftScript.level > 0)
        {
            MinersLocation[slot]++;
            if (MinersLocation[slot] > ShaftScript.level)
                MinersLocation[slot] = 0;
            MinersLocationText[slot].text = MinersLocation[slot].ToString("");
        }
    }

    void UnequipMiner(int slot)
    {
        MinersInBase[MinersEquipped[slot]]++;
        EquippedMinerSprite[slot].sprite = Chegg;
        EquippedMinerButton[slot].interactable = false;
        SlotTaken[slot] = false;
        SlotsTaken--;
        MinersLocationText[slot].text = "";
        //ShaftScript.idlePower -= MLib.Miners[MinersEquipped[slot]].digPower;
        DisplayBase();
    }

    bool LuckCheck(float bonusLuck = 0f)
    {
        totalLuck = luck + bonusLuck;
        if (totalLuck >= Random.Range(0f, 100f + totalLuck))
            return true;
        else return false;
    }
}
