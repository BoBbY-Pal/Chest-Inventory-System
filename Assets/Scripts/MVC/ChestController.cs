using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public ChestModel ChestModel { get; }
    public ChestView ChestView { get; }

    public ChestTypes chestType;
    public int gems;
    public int coins;
    public int unlockTime;
    public int unlockWithGems;
    public string chestStatus;
    
    public bool addedToUnlockingList;
    public bool isStartTime;
    public bool isEmpty;
    public bool isLocked;

    private Sprite _sprite;

    public ChestController(ChestModel chestModel, ChestView chestView, Sprite sprite)
    {
        ChestModel = chestModel;
        ChestView = GameObject.Instantiate<ChestView>(chestView);
        _sprite = sprite;   // Might remove later becoz i'll be playing chest opening animation.
        chestView.SetChestController(this);
    }

    public void InstantiateEmptyChest()
    {
        throw new System.NotImplementedException();
    }

    public void StartUnlocking()
    {
        throw new System.NotImplementedException();
    }
}
