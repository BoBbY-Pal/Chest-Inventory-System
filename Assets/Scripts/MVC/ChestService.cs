
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
    private int chestSlotFull;

    [SerializeField] private ChestTypeSoList _chestTypeSoList;
    
    [SerializeField] private int noOfChests;
    public int noOfChestCanUnlock;
    private ChestView chestToUnlock;
    private List<ChestView> chestUnlockingList;
    [SerializeField] private Button claimChestBtn;

    private int chestCounter;
    public bool isChestTimerStarted { get; set; }

    protected override void Awake()
    {
        base.Awake();
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
            string msg = "Chest slots are full";
            Debug.Log(msg);
            UIHandler.Instance.DisplayMessage(msg);
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
        chestToUnlock._chestController.StartTime();
    }

    public void UnlockUsingGems()
    {
        chestToUnlock._chestController.UnlockUsingGems();
    }

    public void UnlockNextChest(ChestView chestView)
    {
        chestUnlockingList.Remove(chestView);
        if (chestUnlockingList.Count > 0)
        {
            chestUnlockingList[0]._chestController.StartTime();
        }
    }

    public void AddChestToUnlockList()
    {
        string msg;
        if (isChestTimerStarted && noOfChestCanUnlock == chestUnlockingList.Count)
        {
            msg = "Can't unlock more chest!";
            UIHandler.Instance.DisplayMessage(msg);
        }
        else
        {
            msg = "Chest added to the list!";
            UIHandler.Instance.DisplayMessage(msg);
            chestUnlockingList.Add(chestToUnlock);
            chestToUnlock._chestController.ChangeState(ChestState.Unlocking);
        }
    }
}
