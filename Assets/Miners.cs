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
    public int[] MinersEquipped;
    public bool[] SlotTaken;
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

    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            GetRandomWood();
        }
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
        EquippedMinerSprite[openSlot].sprite = MLib.Miners[BaseMinerID[slot]].MinerSprite;
        EquippedMinerButton[openSlot].interactable = true;
        SlotTaken[openSlot] = true;
        SlotsTaken++;
        ShaftScript.idlePower += MLib.Miners[BaseMinerID[slot]].digPower;
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
    }

    void UnequipMiner(int slot)
    {
        MinersInBase[MinersEquipped[slot]]++;
        EquippedMinerSprite[slot].sprite = Chegg;
        EquippedMinerButton[slot].interactable = false;
        SlotTaken[slot] = false;
        SlotsTaken--;
        ShaftScript.idlePower -= MLib.Miners[MinersEquipped[slot]].digPower;
        DisplayBase();
    }
}
