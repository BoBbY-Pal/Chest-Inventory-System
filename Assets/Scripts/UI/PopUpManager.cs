using Chest;
using Enums;
using Project.Utilities;
using Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopUpManager: MonoGenericSingleton<PopUpManager>
    {
        [SerializeField] private GameObject messageScreen;
        [SerializeField] private TextMeshProUGUI messageTxt;
        [SerializeField] private Button timerBtn;
        [SerializeField] private Button gemsBtn;
        [SerializeField] private TextMeshProUGUI headerTxt;
        [SerializeField] private TextMeshProUGUI timerBtnTxt;
        [SerializeField] private TextMeshProUGUI gemsBtnTxt;
        
        public void OnGemsBtnClicked()
        {
            SoundManager.Instance.Play(SoundTypes.ButtonPressed);
            messageScreen.SetActive(false);
            
            ChestService.Instance.OpenChestWithGem();
        }
        
        public void OnTimerBtnClicked()
        {
            SoundManager.Instance.Play(SoundTypes.ButtonPressed);
            messageScreen.SetActive(false);
            
            if (ChestService.Instance.isChestTimerRunning)
            {
                GameLogsManager.CustomLog("Entered onTimer btn clicked and ChesTimerStarted is true");
                ChestService.Instance.AddChestToUnlockList();
            }
            else
            {
                GameLogsManager.CustomLog("Entered onTimer btn clicked and chesTimerStarted is false");
                ChestService.Instance.UnlockChest();
            }
        }

        public async void DisplayChestDetails(string chestType, string coinsRange, string gemsRange)
        {
            messageScreen.SetActive(true);
            timerBtn.gameObject.SetActive(false);
            gemsBtn.gameObject.SetActive(false);
            headerTxt.text = "New Chest!";
            messageTxt.text = $"You got a {chestType} chest.\nCoins: {coinsRange}\nGems: {gemsRange}";

            await new WaitForSeconds(3f);
            messageScreen.SetActive(false);
        }
        
        // Pop message with option buttons.
        public void DisplayMessageWithButton(string header, string msg, int gems, ChestState state)
        {
            messageScreen.SetActive(true);
            timerBtn.gameObject.SetActive(true);
            gemsBtn.gameObject.SetActive(true);
            
            string message;
            if (ChestService.Instance.isChestTimerRunning && ChestService.Instance.noOfChestCanUnlock > 1)
            {
                message = "Add Chest!";
                timerBtnTxt.text = message;
            }
            else
            {
                message = "Start timer";
                timerBtnTxt.text = message;
            }

            headerTxt.text = header;
            messageTxt.text = msg;
            gemsBtnTxt.text = gems.ToString();

            IsChestAdded(state);
        }
        
        private void IsChestAdded(ChestState state)
        {
            // If it's already in unlocking state that means timer is running.
            // That mean we wouldn't show "Start Timer" button.
            timerBtn.gameObject.SetActive(state != ChestState.Unlocking);    
            gemsBtn.gameObject.SetActive(true);
        }
        
        // Pop message without option buttons. Just a popUp message.
        public void DisplayMessage(string header, string msg)
        {
            messageScreen.SetActive(true);
            headerTxt.text = header;
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