using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public ChestController _chestController;

    [SerializeField] private TextMeshProUGUI chestTimerTxt;
    [SerializeField] private TextMeshProUGUI chestStatusTxt;
    [SerializeField] private TextMeshProUGUI chestTypeTxt;
    [SerializeField] private Image chestSpriteSlot;

    public static event Action OnChestButtonPressed;

    void Start()
    {
        SetParent();
        DisplayChest();
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

    public void Initialize(ChestController chestController)
    {
        _chestController = chestController;
        Debug.Log("Controller initialized");
        
        
    }

    public void DisplayChest()
    {
        

        chestTimerTxt.text = _chestController.TimeToString();
        chestTypeTxt.text = _chestController.ChestModel.ChestType.ToString();
        chestStatusTxt.text = _chestController.GetState.ToString();

        if (_chestController.GetState == ChestState.Locked || _chestController.GetState == ChestState.Unlocking)
        {
            chestSpriteSlot.sprite = _chestController.ChestModel.lockedChestSprite;
        }
        else
        {
            chestSpriteSlot.sprite = _chestController.ChestModel.unlockedChestSprite;
        }

    }

    public void ChestButtonPressed()
    {
        OnChestButtonPressed?.Invoke();
    }

    public void ShowUnlockTime(float time)
    {
        chestTimerTxt.text = time.ToString();
    }
    

    public void DestroyChest()
    {
        Destroy(gameObject);
        ChestService.Instance.chestCounter--;
    }
}
