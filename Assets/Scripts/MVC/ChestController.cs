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
    }

    public void StartUnlocking()
    {
        ChestView.ShowUnlockGems(ChestModel.GemsRequiredToUnlock);
        ChestView.ShowUnlockTime(ChestModel.unlockTime);
        if (ChestModel.unlockTime <= 0)
        {
            ChestUnlocked();
        }
    }

    public void ChestBtnPressed()
    {
        string msg;
        
        if (CheckState(ChestState.Locked))
        {
            msg = "Unlock this chest";
            ChestService.Instance.SetChestView(ChestView);
            UIHandler.Instance.DisplayMessageWithButton(msg, ChestModel.GemsRequiredToUnlock, ChestState.Unlocking);
        }
        else
        {
            PlayerInventory.Instance.UpdatePlayerInventory(ChestModel.coins, ChestModel.gems);
            msg = $"{ChestModel.coins} coins and {ChestModel.gems} gems added to the inventory!";
            UIHandler.Instance.DisplayMessage(msg);
            ChestView.DestroyChest();
        }
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
