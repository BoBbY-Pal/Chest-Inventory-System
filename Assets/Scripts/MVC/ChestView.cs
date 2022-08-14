using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    public ChestController _chestController;

    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private TextMeshProUGUI chestStatusTxt;
    [SerializeField] private TextMeshProUGUI chestTypeTxt;
    [SerializeField] private Image chestSpriteSlot;

    private float _time;
    public static event Action OnChestButtonPressed;

    void Start()
    {
        SetParent();
        DisplayChest();
    }

    void Update()
    {
        if (_chestController.GetCurrentState == ChestState.Unlocking)
        {
            DecreaseTimer();
            if (IsTimeOver())
            {
                timerTxt.text = "READY";
                _chestController.ChangeState(ChestState.Unlocked);
                _chestController.StartUnlocking();
            }

        }
        
        // if (_chestController.isStartTime)
        // {
        //     _chestController.StartUnlocking();
        // }
    }

    private void DecreaseTimer()
    {
        _time -= Time.deltaTime;
        timerTxt.text = TimeToString(_time);
    }

    private string TimeToString(float value)
    {
        TimeSpan time = TimeSpan.FromSeconds(value);
        string timeString = time.ToString(@"hh\:mm\:ss");
        return timeString;
    }

    private bool IsTimeOver() => _time <= 0;
    private void SetParent()
    {
        transform.SetParent(ChestService.Instance.chestSlotGroup.transform);
    }

    public void Initialize(ChestController chestController, float time)
    {
        _chestController = chestController;
        this._time = time;
        Debug.Log("Controller initialized");
    }

    public void DisplayChest()
    {
        timerTxt.text = TimeToString(_time);
        chestTypeTxt.text = _chestController.ChestModel.ChestType.ToString();
        chestStatusTxt.text = _chestController.GetCurrentState.ToString();

        if (_chestController.GetCurrentState == ChestState.Locked || _chestController.GetCurrentState == ChestState.Unlocking)
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

    public void DestroyChest()
    {
        Destroy(gameObject);
        ChestService.Instance.chestCounter--;
    }
}
