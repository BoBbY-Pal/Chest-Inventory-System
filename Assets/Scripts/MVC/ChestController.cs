using DefaultNamespace;
using Enums;
using ScriptableObjects;
using UI;
using UnityEngine;
using UnityEngine.PlayerLoop;


public enum ChestState
{
    Locked,
    Unlocking,
    Unlocked
}
public class ChestController : MonoBehaviour
{
    public ChestModel ChestModel { get; }
    private ChestView ChestView { get; }

    private ChestState _state;

    public ChestState GetState => _state;
    public void ChangeState(ChestState chestState) => _state = chestState;
    public bool CheckState(ChestState chestState) => _state == chestState;

    // public ChestTypes chestType;
    // public int gems;
    // public int coins;
    // public int unlockTime;
    // public int unlockWithGems;
    // public string chestStatus;
    //
    // public bool addedToUnlockingList;
    public bool isStartTime;
    // public bool isEmpty;
    // public bool isLocked;

    // private Sprite _sprite;

    public ChestController(ChestModel chestModel, ChestView chestView)
    {
        ChestModel = chestModel;
        chestView.Initialize(this);
        ChestView = GameObject.Instantiate<ChestView>(chestView);
        ChangeState(ChestState.Locked);
        chestView.DisplayChest();
       
    }

    // public void SetupEmptyChest()
    // {
    //     // ChestModel.coins = 0;
    //     // ChestModel.gems = 0;
    //     // ChestModel.GemsRequiredToUnlock = 
    //
    //     coins = 0;
    //     gems = 0;
    //     unlockWithGems = 0;
    //     chestType = ChestTypes.None;
    //     chestStatus = "Empty";
    //     isEmpty = true;
    //     addedToUnlockingList = false;
    //     ChestView.DisplayChest();
    // }

    public void StartUnlocking()
    {
        throw new System.NotImplementedException();
    }

    public void ChestBtnPressed()
    {
        string msg;
        // if (isEmpty)
        // {
        //     msg = "This Chest slot is Empty!";
        //     // UIHandler.Instance.DisplayMsg(msg);
        //     return;
        // }
        // else 
        if (CheckState(ChestState.Locked))
        {
            msg = "Unlock this chest";
            // ChestService.Instance.SetChestView(ChestView);
        }
    }

    // public void AddChest(ChestTypeSo chestTypeSo)
    // {
    //     chestType = chestTypeSo.chestType;
    //     coins = Random.Range(chestTypeSo.coinRange.min, chestTypeSo.coinRange.max +1);
    //     gems = Random.Range(chestTypeSo.gemRange.min, chestTypeSo.gemRange.max +1);
    //     unlockTime = chestTypeSo.unlockTime;
    //     chestStatus = "locked";
    //     isEmpty = false;
    //     isLocked = true;
    //     unlockWithGems = 50;
    //     ChestView.DisplayChest();
    // }
    public void UnlockUsingGems()
    {
        bool b_canUnlock = PlayerInventory.Instance.RemoveGems(ChestModel.GemsRequiredToUnlock);
        if (b_canUnlock)
        {
            ChestUnlocked();
        }
        else
        {
            string msg = "You Don't have enough gems!";
            UIHandler.Instance.DisplayMessage(msg);
        }
    }

    private void ChestUnlocked()
    {
        ChangeState(ChestState.Unlocked);
        ChestModel.GemsRequiredToUnlock = 0;
        ChestService.Instance.isChestTimerStarted = false;
        ChestModel.unlockTime = 0;
        ChestView.DisplayChest();
        ChestService.Instance.UnlockNextChest(ChestView);
    }

    public async void StartTime()
    {
        isStartTime = true;
        ChestService.Instance.isChestTimerStarted = true;
        while (ChestModel.unlockTime > 0)
        {
            await new WaitForSeconds(1f);
            ChestModel.unlockTime -= 1;
        }
    }
}
