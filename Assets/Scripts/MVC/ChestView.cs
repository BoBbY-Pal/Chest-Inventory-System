using System;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public ChestController chestController;
    private float time;

    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI chestStatusTxt;
    [SerializeField] private TextMeshProUGUI chestTypeTxt;
    [SerializeField] private Image chestSpriteSlot;
    public event Action OnChestButtonPressed;


    void Start()
    {
        SetParent();
    }

    void Update()
    {
        if (chestController.GetCurrentState == ChestState.Unlocking)
        {
            DecreaseTimer();
            chestStatusTxt.text = chestController.GetCurrentState.ToString();
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

    public void Initialize(ChestController chestController, float time)
    {
        this.chestController = chestController;
        this.time = time;
        GameLogsManager.CustomLog("Controller initialized");
    }

    public void DisplayChest(float chestTime, ChestTypes chestType, Sprite lockedChestSprite, Sprite unlockedChestSprite)
    {
        time = chestTime;
        timerTxt.text = TimeToString(time);
        chestTypeTxt.text = chestType.ToString();
        chestStatusTxt.text = chestController.GetCurrentState.ToString();

        if (chestController.GetCurrentState == ChestState.Locked || chestController.GetCurrentState == ChestState.Unlocking)
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
        time -= Time.deltaTime;
        timerTxt.text = TimeToString(time);
    }

    private string TimeToString(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        string timeString = time.ToString(@"hh\:mm\:ss");
        return timeString;
    }

    private bool IsTimeOver() => time <= 0;
    
    public void ChestButtonPressed()
    {
        OnChestButtonPressed?.Invoke();
    }

    public void DestroyChest()
    {
        Destroy(gameObject);
        ChestService.Instance.chestCounter--;
    }
}
