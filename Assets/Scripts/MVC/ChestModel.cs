using Enums;
using ScriptableObjects;
using UnityEngine;

public class ChestModel
{
    private ChestController _chestController;
    
    public ChestTypes ChestType { get; private set; }
    public int coins { get; private set; }
    public int gems { get; private set; }
    public int unlockTime { get; private set; }

    public ChestModel(ChestSO chestSo, ChestController chestController)
    {
        _chestController = chestController;
        ChestType = chestSo.chestType;
        unlockTime = chestSo.unlockTime;
        coins = Random.Range(chestSo.minCoins, chestSo.maxCoins);
        gems = Random.Range(chestSo.minGems, chestSo.maxGems);
    }
}
