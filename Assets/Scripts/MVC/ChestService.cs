using System.Collections.Generic;
using ScriptableObjects;
using Singleton;
using UI;
using UnityEngine;

using Button = UnityEngine.UI.Button;


public class ChestService : MonoGenericSingleton<ChestService>
{
    private ChestController[] chests;
    [SerializeField] private ChestView chestPrefab;
    
    public GameObject chestSlotGroup;

    [SerializeField] private ChestTypeSoList _chestTypeSoList;
    
    [SerializeField] private int noOfChests;
    public int noOfChestCanUnlock;
    private ChestView chestToUnlock;
    private List<ChestView> chestUnlockingList;
    [SerializeField] private Button claimChestBtn;

    public int chestCounter;
    public bool isChestTimerStarted { get; set; }

    protected override void Awake()
    {
        base.Awake();
        chestUnlockingList = new List<ChestView>();
        claimChestBtn.onClick.AddListener(SpawnChest);
        
    }

    private void SpawnChest()
    {
        chests = new ChestController[noOfChests];
        int randomNum = Random.Range(0, _chestTypeSoList.chestsTypeList.Length);
        
        
        if (chestCounter < chests.Length)
        {
            Debug.Log(chestCounter);
            chests[chestCounter] = CreateChest(_chestTypeSoList.chestsTypeList[randomNum]);
            chestCounter++;
        }
        else
        {
            string msg = "All chest slots are full";
            string header = "No More Space!";
            Debug.Log(msg);
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

    public void UnlockChest()
    {
        chestUnlockingList.Add(chestToUnlock);
        chestToUnlock._chestController.ChangeState(ChestState.Unlocking);
    }

    public void UnlockNextChest(ChestView chestView)
    {
        chestUnlockingList.Remove(chestView);
        if (chestUnlockingList.Count > 0)
        {
       
            chestUnlockingList[0]._chestController.ChangeState(ChestState.Unlocking);
        }
    }

    public void AddChestToUnlockList()
    {
        string msg;
        string header;
        if (isChestTimerStarted && noOfChestCanUnlock == chestUnlockingList.Count)
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
            chestToUnlock._chestController.ChangeState(ChestState.Unlocking);
        }
    }
}
