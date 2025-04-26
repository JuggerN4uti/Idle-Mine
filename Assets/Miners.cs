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

    [Header("Crafting")]
    public bool maceSelected;
    public Image MaceSprite;
    public int[] CraftCharges;
    public Image[] CraftProgress;

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
        Invoke("AutoDig", 0.75f);
    }

    void AutoDig()
    {
        for (int i = 0; i < MinersEquipped.Length; i++)
        {
            if (SlotTaken[i])
                StartCoroutine(Dig(i, i * 0.075f));
            //Dig(i);
        }
        Invoke("AutoDig", 0.75f);
    }

    IEnumerator Dig(int miner, float timer)
    {
        yield return new WaitForSeconds(timer);
        if (MinersLocation[miner] == 0)
            ShaftScript.Dig(MLib.Miners[MinersEquipped[miner]].digPower * 1.55f);
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
                        MinersInBase[MLib.RollDiamondMiner()]++;
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
        {
            MinersHUD.SetActive(false);
            //maceSelected = false;
        }
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
            if (SlotsTaken < MinersMaxSlots || maceSelected)
                BaseMinerButton[i].interactable = true;
            else BaseMinerButton[i].interactable = false;
        }

        for (int i = currentPlace; i < BaseMinerObject.Length; i++)
        {
            BaseMinerObject[i].SetActive(false);
        }
    }

    public void SelectMace()
    {
        if (maceSelected)
        {
            maceSelected = false;
            MaceSprite.color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            maceSelected = true;
            MaceSprite.color = new Color(0.8f, 0f, 0f, 1f);
        }
        DisplayBase();
    }

    public void EquipMiner(int slot)
    {
        if (maceSelected)
        {
            if (MLib.Miners[BaseMinerID[slot]].tier < 4)
            {
                MinersInBase[BaseMinerID[slot]]--;
                CraftCharges[MLib.Miners[BaseMinerID[slot]].tier]++;
                if (CraftCharges[MLib.Miners[BaseMinerID[slot]].tier] >= 6)
                    CraftNewMiner(MLib.Miners[BaseMinerID[slot]].tier);
                CraftProgress[MLib.Miners[BaseMinerID[slot]].tier].fillAmount = CraftCharges[MLib.Miners[BaseMinerID[slot]].tier] * 0.175f;
            }
        }
        else
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
        }
        DisplayBase();
    }

    void CraftNewMiner(int tier)
    {
        CraftCharges[tier] -= 6;
        switch (tier)
        {
            case 0:
                MinersInBase[MLib.RollStoneMiner()]++;
                break;
            case 1:
                MinersInBase[MLib.RollIronMiner()]++;
                break;
            case 2:
                MinersInBase[MLib.RollGoldMiner()]++;
                break;
            case 3:
                MinersInBase[MLib.RollDiamondMiner()]++;
                break;
        }
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
