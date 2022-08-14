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
        ChestView.DisplayChest(ChestModel.unlockTime);
        SubscribeEvents();
        
    }

    private void SubscribeEvents()
    {
        ChestView.OnChestButtonPressed += ChestBtnPressed;
        UIHandler.OnUnlockUsingGem += UnlockUsingGems;
    }

    private void UnSubscribeEvents()
    {
        ChestView.OnChestButtonPressed -= ChestBtnPressed;
        UIHandler.OnUnlockUsingGem -= UnlockUsingGems;
    }
    
    public void StartUnlocking()
    {
        
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
            UnSubscribeEvents();
            ChestView.DestroyChest();
        }
    }

    private void UnlockUsingGems()
    {
        bool canUnlock = PlayerInventory.Instance.DeductGems(ChestModel.GemsRequiredToUnlock);
        if (canUnlock)
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
        ChestView.DisplayChest(ChestModel.unlockTime);
        ChestService.Instance.UnlockNextChest(ChestView);
    }

    public void StartTimer()
    {
        
        isStartTime = true;
        ChestService.Instance.isChestTimerStarted = true;
        while (ChestModel.unlockTime > 0)
        {
            ChestModel.unlockTime -= Time.deltaTime;
            string value = TimeToString(ChestModel.unlockTime);
            ChestView.UpdateTime(value);
        }
    }
      
    public string TimeToString(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        string timeString = time.ToString(@"hh\:mm\:ss");
        return timeString;
    }
}
