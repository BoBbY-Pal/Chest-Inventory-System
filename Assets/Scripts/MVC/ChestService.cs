using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using Singleton;
using UI;
using UnityEngine;
using Button = UnityEngine.UI.Button;


namespace Chest
{
    public class ChestService : MonoGenericSingleton<ChestService>
    {
        private ChestController[] chests;
        [SerializeField] private ChestView chestPrefab;
    
        public GameObject chestSlotGroup;

        [SerializeField] private ChestTypeSoList chestTypeSoList;
    
        [SerializeField] private int noOfChests;
        public int noOfChestCanUnlock;
        private ChestView chestToUnlock;
        private List<ChestView> chestUnlockingList;
        [SerializeField] private Button claimChestBtn;

        public int chestCounter;
        public bool isChestTimerRunning { get; set; }

        protected override void Awake()
        {
            base.Awake();
            chestUnlockingList = new List<ChestView>();
            claimChestBtn.onClick.AddListener(SpawnChest);
        
        }

        private void SpawnChest()
        {
            SoundManager.Instance.Play(SoundTypes.ButtonPressed);
        
            chests = new ChestController[noOfChests];
            int randomNum = Random.Range(0, chestTypeSoList.chestsTypeList.Length);
        
        
            if (chestCounter < chests.Length)
            {
                GameLogsManager.CustomLog(chestCounter);;
                chests[chestCounter] = CreateChest(chestTypeSoList.chestsTypeList[randomNum]);
                chestCounter++;
            }
            else
            {
                string msg = "All chest slots are full";
                string header = "No More Space!";
                GameLogsManager.CustomLog(msg);
                PopUpManager.Instance.DisplayMessage(header, msg);
            }
        
        }
        private ChestController CreateChest(ChestTypeSo chestTypeSo)
        {
        
            ChestModel chestModel = new ChestModel(chestTypeSo);
            ChestController chestController = new ChestController(chestModel, chestPrefab);
            return chestController;
        }

        public void SetChestView(ChestView view)
        {
            chestToUnlock = view;
        }

        public void OpenChestWithGem()
        {
            chestToUnlock.chestController.UnlockUsingGems();
        }
    
        public void UnlockChest()
        {
            chestUnlockingList.Add(chestToUnlock);
            chestToUnlock.chestController.currentState = ChestState.Unlocking;
        
            isChestTimerRunning = true;
        }

        public void UnlockNextChest(ChestView chestView)
        {
            chestUnlockingList.Remove(chestView);
            if (chestUnlockingList.Count > 0)
            {
                isChestTimerRunning = true;
                chestUnlockingList[0].chestController.currentState = ChestState.Unlocking;
            }
        }

        public void AddChestToUnlockList()
        {
            string msg;
            string header;
            if (isChestTimerRunning && noOfChestCanUnlock+1 == chestUnlockingList.Count)
            {
                header = "No More Space!";
                msg = "Can't unlock more chest!";
                PopUpManager.Instance.DisplayMessage(header, msg);
            }
            else
            {
                header = "Unlock Chest!";
                msg = "Chest added to the list!";
                PopUpManager.Instance.DisplayMessage(header, msg);
                chestUnlockingList.Add(chestToUnlock);
            }
        }
    }
}
