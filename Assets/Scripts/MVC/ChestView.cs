using System;
using Enums;
using Project.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chest
{
    public class ChestView : MonoBehaviour
    {
        public ChestController chestController;
        private float chestLocaltime;

        [SerializeField] private TextMeshProUGUI timerTxt;
        [SerializeField] private TextMeshProUGUI chestStatusTxt;
        [SerializeField] private TextMeshProUGUI chestTypeTxt;
        [SerializeField] private Image chestSpriteSlot;
        [SerializeField] private Button chestButton; 
        public event Action OnChestButtonPressed;


        private void Awake()
        {
            chestButton.onClick.AddListener(ChestButtonPressed);
        }

        private void Start()
        {
            SetParent();
        }

        private void Update()
        {
            if (chestController.currentState == ChestState.Unlocking)
            {
                DecreaseTimer();
                chestStatusTxt.text = chestController.currentState.ToString();
                if (IsTimeOver())
                {
                    timerTxt.text = "READY";
                    chestController.ChestUnlocked();
                }
            }
        }
    
        private void SetParent()
        {
            transform.SetParent(ChestService.Instance.chestSlotGroup.transform);
        }

        public void Initialize(ChestController controller, float time)
        {
            chestController = controller;
            chestLocaltime = time;
            GameLogsManager.CustomLog("Controller initialized");
        }

        public void DisplayChest(float chestTime, ChestTypes chestType, Sprite lockedChestSprite, Sprite unlockedChestSprite)
        {
            chestLocaltime = chestTime;
            timerTxt.text = TimeToString(chestLocaltime);
            chestTypeTxt.text = chestType.ToString();
            chestStatusTxt.text = chestController.currentState.ToString();

            if (chestController.currentState == ChestState.Locked || chestController.currentState == ChestState.Unlocking)
            {
                chestSpriteSlot.sprite = lockedChestSprite;
            }
            else
            {
                chestSpriteSlot.sprite = unlockedChestSprite;
            }

        }

        private void DecreaseTimer()
        {
            chestLocaltime -= Time.deltaTime;
            timerTxt.text = TimeToString(chestLocaltime);
        }

        private string TimeToString(float value)
        {
            TimeSpan time = TimeSpan.FromSeconds(value);
            string timeString = time.ToString(@"hh\:mm\:ss");
            return timeString;
        }

        private bool IsTimeOver() => chestLocaltime <= 0;
    
        private void ChestButtonPressed()
        {
            OnChestButtonPressed?.Invoke();
        }

        public void DestroyChest()
        {
            Destroy(gameObject);
            ChestService.Instance.chestCounter--;
        }
    }
}
