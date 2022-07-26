using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestView : MonoBehaviour
{
    private ChestController _chestController;

    public Sprite lockedChestSprite;
    public Sprite unlockedChestSprite;
    public bool isChestSprite;

    [SerializeField] private TextMeshPro chestTimer;
    [SerializeField] private TextMeshPro unlockGems;
    [SerializeField] private TextMeshPro chestStatus;
    [SerializeField] private TextMeshPro chestType;
    [SerializeField] private Image chestSlot;

    void Start()
    {
        makeParent();
        _chestController.InstantiateEmptyChest();
    }

    void Update()
    {
        if (_chestController.isStartTime)
        {
            _chestController.StartUnlocking();
        }
    }
    private void makeParent()
    {
        // transform.SetParent(ChestService.Instance.ches);
    }

    public void SetChestController(ChestController chestController) => _chestController = chestController;
    
}
