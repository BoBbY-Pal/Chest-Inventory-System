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
        [SerializeField] private TextMeshProUGUI headerTxt;
        [SerializeField] private TextMeshProUGUI timerBtnTxt;
        [SerializeField] private TextMeshProUGUI gemsBtnTxt;

        public static event System.Action OnUnlockUsingGem;
        public void OnGemsBtnClicked()
        {
            messageScreen.SetActive(false);
            OnUnlockUsingGem?.Invoke();
        }
        
        public void OnTimerBtnClicked()
        {
            messageScreen.SetActive(false);
            
            if (ChestService.Instance.isChestTimerStarted)
            {
                Debug.Log("Entered onTimer btn clicked and chesTimerStarted is true");
                ChestService.Instance.AddChestToUnlockList();
            }
            else
            {
                Debug.Log("Entered onTimer btn clicked and chesTimerStarted is false");
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
        
        public void DisplayMessageWithButton(string header, string msg, int gems, ChestState state)
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

            headerTxt.text = header;
            messageTxt.text = msg;
            gemsBtnTxt.text = gems.ToString();

            IsChestAdded(state);
        }
        
        private void IsChestAdded(ChestState state)
        {
            if (state == ChestState.Unlocking)
            {
                timerBtn.gameObject.SetActive(false);
            }
            else
            {
                timerBtn.gameObject.SetActive(true);
            }
            
            gemsBtn.gameObject.SetActive(true);
        }
        
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