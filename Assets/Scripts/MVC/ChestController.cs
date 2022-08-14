using System;
using DefaultNamespace;
using UI;
using UnityEngine;

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

    public bool isStartTime;
   

    public ChestController(ChestModel chestModel, ChestView view)
    {
        ChestModel = chestModel;
        ChestView = GameObject.Instantiate<ChestView>(view);
        ChestView.Initialize(this);
        ChangeState(ChestState.Locked);
        UIHandler.Instance.DisplayChestDetails(ChestModel.ChestType.ToString(), ChestModel.coinsRange, ChestModel.gemsRange);
        ChestView.OnChestButtonPressed += ChestBtnPressed;
    }

    public void StartUnlocking()
    {
        
        ChestView.ShowUnlockTime(ChestModel.unlockTime);
        if (ChestModel.unlockTime <= 0)
        {
            ChestUnlocked();
        }
    }

    private void ChestBtnPressed()
    {
        string msg;
        string header;
        
        if (CheckState(ChestState.Locked))
        {
            msg = "Please select how you want to open the chest";
            header = "Unlock Chest!";
            ChestService.Instance.SetChestView(ChestView);
            UIHandler.Instance.DisplayMessageWithButton(header, msg, ChestModel.GemsRequiredToUnlock, ChestState.Unlocking);
        }
        else
        {
            
            PlayerInventory.Instance.UpdatePlayerInventory(ChestModel.coins, ChestModel.gems);
            msg = $"{ChestModel.coins} coins and {ChestModel.gems} gems added to the inventory!";
            header = "Congratulations!";
            UIHandler.Instance.DisplayMessage(header, msg);
            ChestView.OnChestButtonPressed -= ChestBtnPressed;
            ChestView.DestroyChest();
        }
    }
    
    public string TimeToString()
    {
        TimeSpan time = TimeSpan.FromSeconds(ChestModel.unlockTime);
        string timeString = time.ToString(@"hh\:mm\:ss");
        return timeString;
    }
    
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
            string header = "Oops!";
            UIHandler.Instance.DisplayMessage(header, msg);
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

    public void StartTimer()
    {
        
        isStartTime = true;
        ChestService.Instance.isChestTimerStarted = true;
        while (ChestModel.unlockTime > 0)
        {
            ChestModel.unlockTime -= Time.deltaTime;
        }
    }
}
