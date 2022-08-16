using DefaultNamespace;
using UI;
using UnityEngine;

public enum ChestState
{
    Locked,
    Unlocking,
    Unlocked
}
[System.Serializable]
public class ChestController
{
    public ChestModel ChestModel { get; }
    private ChestView ChestView { get; }

    private ChestState currentState;

    public ChestState GetCurrentState => currentState;
    public void ChangeState(ChestState chestState) => currentState = chestState;


    public ChestController(ChestModel chestModel, ChestView view)
    {
        ChestModel = chestModel;
        ChestView = GameObject.Instantiate<ChestView>(view);
        
        ChestView.Initialize(this, ChestModel.unlockTime);
        ChangeState(ChestState.Locked);
        PopUpManager.Instance.DisplayChestDetails(ChestModel.ChestType.ToString(), ChestModel.coinsRange, ChestModel.gemsRange);
        SubscribeEvents();
        
    }

    private void SubscribeEvents()
    {
        ChestView.OnChestButtonPressed += ChestBtnPressed;
        PopUpManager.Instance.OnUnlockUsingGem += UnlockUsingGems;
    }

    private void UnSubscribeEvents()
    {
        ChestView.OnChestButtonPressed -= ChestBtnPressed;
        PopUpManager.Instance.OnUnlockUsingGem -= UnlockUsingGems;
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
        
        switch (GetCurrentState)
        {
            case ChestState.Locked:
                msg = "Please select how you want to open the chest";
                header = "Unlock Chest!";
                ChestService.Instance.SetChestView(ChestView);
                PopUpManager.Instance.DisplayMessageWithButton(header, msg, ChestModel.GemsRequiredToUnlock, GetCurrentState);
                break;
            
            case ChestState.Unlocking:
                msg = $"Do you want to unlock it now for {ChestModel.GemsRequiredToUnlock} gems?";
                header = "Unlocking!";
                PopUpManager.Instance.DisplayMessageWithButton(header, msg, ChestModel.GemsRequiredToUnlock, GetCurrentState);
                break;

            case ChestState.Unlocked:
            default:
                PlayerInventory.Instance.UpdatePlayerInventory(ChestModel.coins, ChestModel.gems);
                msg = $"{ChestModel.coins} coins and {ChestModel.gems} gems added to the inventory!";
                header = "Congratulations!";
                PopUpManager.Instance.DisplayMessage(header, msg);
                UnSubscribeEvents();
                ChestView.DestroyChest();
                break;
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
            PopUpManager.Instance.DisplayMessage(header, msg);
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
}
