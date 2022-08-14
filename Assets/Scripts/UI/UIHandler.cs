using System.Collections;
using ScriptableObjects;
using Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHandler: MonoGenericSingleton<UIHandler>
    {
        [SerializeField] private GameObject messageScreen;
        [SerializeField] private TextMeshProUGUI messageTxt;
        [SerializeField] private Button timerBtn;
        [SerializeField] private Button gemsBtn;
        [SerializeField] private TextMeshProUGUI header;
        [SerializeField] private TextMeshProUGUI timerBtnTxt;
        [SerializeField] private TextMeshProUGUI gemsBtnTxt;

        public void OnGemsBtnClicked()
        {
            messageScreen.SetActive(false);
            ChestService.Instance.UnlockUsingGems();
        }
        
        public void OnTimerBtnClicked()
        {
            messageScreen.SetActive(false);
            if (ChestService.Instance.isChestTimerStarted)
            {
                ChestService.Instance.AddChestToUnlockList();
            }
            else
            {
                ChestService.Instance.UnlockChest();
            }
        }
        
        public void DisplayMessageWithButton(string msg, int gems, ChestState state)
        {
            messageScreen.SetActive(true);
            timerBtn.gameObject.SetActive(true);
            gemsBtn.gameObject.SetActive(true);
            
            string message;
            if (ChestService.Instance.isChestTimerStarted && ChestService.Instance.noOfChestCanUnlock > 1)
            {
                message = "Add Chest!";
                timerBtnTxt.text = message;
            }
            else
            {
                message = "Start timer";
                timerBtnTxt.text = message;
            }

            gemsBtnTxt.text = gems.ToString();
            messageTxt.text = msg;

            IsChestAdded(state);
        }

        private void IsChestAdded(ChestState state)
        {
            if (state == ChestState.Unlocking)
            {
                timerBtnTxt.gameObject.SetActive(false);
            }
            else
            {
                timerBtn.gameObject.SetActive(true);
            }
            
            gemsBtn.gameObject.SetActive(true);
        }

        public async void DisplayChestDetails(string chestType, string coinsRange, string gemsRange)
        {
            messageScreen.SetActive(true);
            timerBtn.gameObject.SetActive(false);
            gemsBtn.gameObject.SetActive(false);
            header.text = "New Chest!";
            messageTxt.text = $"You got a {chestType} chest.\nCoins: {coinsRange}\nGems: {gemsRange}";

            await new WaitForSeconds(3f);
            messageScreen.SetActive(false);
        }
        
        
        public void DisplayMessage(string msg)
        {
            messageScreen.SetActive(true);
            messageTxt.text = msg;
            timerBtn.gameObject.SetActive(false);
            gemsBtn.gameObject.SetActive(false);
            DisableMessage();
        }

        private async void DisableMessage()
        {
            await new WaitForSeconds(2f);
            messageScreen.gameObject.SetActive(false);
        }
    }
}