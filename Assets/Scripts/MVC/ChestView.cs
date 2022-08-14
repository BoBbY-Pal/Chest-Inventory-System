
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public ChestController _chestController;

    
    public bool isChestSprite;
    // public Image chestSprite;
    
    [SerializeField] private TextMeshProUGUI chestTimerTxt;
    [SerializeField] private Text unlockGemsTxt;
    [SerializeField] private TextMeshProUGUI chestStatusTxt;
    [SerializeField] private TextMeshProUGUI chestTypeTxt;
    [SerializeField] private Image chestSpriteSlot;

   

    void Start()
    {
        SetParent();
        
        // _chestController.SetupEmptyChest();
    }

    void Update()
    {
        // if (_chestController.GetState == ChestState.Unlocking)
        // {
        //     DecreaseTimer()
        //         
        // }
        if (_chestController.isStartTime)
        {
            _chestController.StartUnlocking();
        }
    }
    
    private void SetParent()
    {
        transform.SetParent(ChestService.Instance.chestSlotGroup.transform);
    }

    public void Initialize(ChestController chestController) => _chestController = chestController;

    public void DisplayChest()
    {
        // if (_chestController.ChestModel.unlockTime <= 0)
        // {
        //     _chestController.ChestModel.unlockTime = 0;
        // }
        
        chestTimerTxt.text = _chestController.ChestModel.unlockTime.ToString();
        unlockGemsTxt.text = _chestController.ChestModel.GemsRequiredToUnlock.ToString();
        chestTypeTxt.text = _chestController.ChestModel.ChestType.ToString();
        chestStatusTxt.text = _chestController.GetState.ToString();

        if (_chestController.GetState == ChestState.Locked )
        {
            chestSpriteSlot.sprite = _chestController.ChestModel.lockedChestSprite;
        }
        else
        {
            isChestSprite = true;
            chestSpriteSlot.sprite = _chestController.ChestModel.unlockedChestSprite;
        }

    }

    public void OnChestBtnPressed()
    {
        _chestController.ChestBtnPressed();
    }

    public void ShowUnlockTime(int time)
    {
        chestTimerTxt.text = time.ToString();
    }

    public void ShowUnlockGems(int gems)
    {
        unlockGemsTxt.text = gems.ToString();
    }
    
}
