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
        [SerializeField] private TextMeshProUGUI timerBtnTxt;
        [SerializeField] private TextMeshProUGUI gemsBtnTxt;

        public void OnGemsBtnClicked()
        {
            messageScreen.SetActive(false);
            ChestService.Instance.UnlockUsingGems();
        }

        public void DisplayMessageWithButton(string msg, int gems, bool isChestInList)
        {
            messageScreen.SetActive(true);
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

            IsChestAdded(isChestInList);
        }

        private void IsChestAdded(bool isChestInList)
        {
            if (isChestInList)
            {
                timerBtnTxt.gameObject.SetActive(false);
            }
            else
            {
                timerBtn.gameObject.SetActive(true);
            }
            
            gemsBtn.gameObject.SetActive(true);
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