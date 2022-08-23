using Enums;
using UI;
using UnityEngine;

namespace Chest
{
    public class ChestController
    {
        private ChestModel ChestModel { get; }
        private ChestView ChestView { get; }

        public ChestState currentState { get; set; }

        public ChestController(ChestModel model, ChestView view)
        {
            ChestModel = model;
            ChestView = Object.Instantiate(view);
        
            ChestView.Initialize(this, ChestModel.unlockTime);
            // ChangeState(ChestState.Locked);
            currentState = ChestState.Locked;
            PopUpManager.Instance.DisplayChestDetails(ChestModel.ChestType.ToString(), ChestModel.coinsRange, ChestModel.gemsRange);
            ChestView.DisplayChest(ChestModel.unlockTime, ChestModel.ChestType, ChestModel.lockedChestSprite, ChestModel.unlockedChestSprite);
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ChestView.OnChestButtonPressed += ChestBtnPressed;
        }

        private void UnSubscribeEvents()
        {
            ChestView.OnChestButtonPressed -= ChestBtnPressed;
        }

        private void ChestBtnPressed()
        {
            SoundManager.Instance.Play(SoundTypes.ButtonPressed);
        
            string msg;
            string header;
        
            switch (currentState)
            {
                case ChestState.Locked:
                    msg = "Please select how you want to open the chest";
                    header = "Unlock Chest!";
                    ChestService.Instance.SetChestView(ChestView);
                    PopUpManager.Instance.DisplayMessageWithButton(header, msg, ChestModel.GemsRequiredToUnlock, currentState);
                    break;
            
                case ChestState.Unlocking:
                    msg = $"Do you want to unlock it now for {ChestModel.GemsRequiredToUnlock} gems?";
                    header = "Unlocking!";
                    PopUpManager.Instance.DisplayMessageWithButton(header, msg, ChestModel.GemsRequiredToUnlock, currentState);
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

        public void UnlockUsingGems()
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

        public void ChestUnlocked()
        {
            // ChangeState(ChestState.Unlocked);
            currentState = ChestState.Unlocked;
            ChestModel.unlockTime = 0;
            ChestModel.GemsRequiredToUnlock = 0;
            ChestService.Instance.isChestTimerRunning = false;
            ChestView.DisplayChest(ChestModel.unlockTime, ChestModel.ChestType, ChestModel.lockedChestSprite, ChestModel.unlockedChestSprite);
            ChestService.Instance.UnlockNextChest(ChestView);
        }
    }
}
