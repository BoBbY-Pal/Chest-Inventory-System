using Enums;
using ScriptableObjects;
using UnityEngine;
    

public class ChestModel
{
    public ChestTypes ChestType { get; }
    public int coins { get; }
    public string coinsRange { get; }
    public int gems { get;   }
    public string gemsRange { get; }
    
    public float unlockTime { get; }
    public int GemsRequiredToUnlock { get; set; }
    
    public Sprite lockedChestSprite { get; }
    public Sprite unlockedChestSprite { get; }

    public ChestModel(ChestTypeSo chestTypeSo)
    {
        unlockedChestSprite = chestTypeSo.unlockedChestSprite;
        lockedChestSprite = chestTypeSo.lockedChestSprite;
        ChestType = chestTypeSo.chestType;
        unlockTime = chestTypeSo.unlockTime;
        GemsRequiredToUnlock = chestTypeSo.gemsRequiredToUnlock;
        
        coins = Random.Range(chestTypeSo.coinRange.min, chestTypeSo.coinRange.max);
        gems = Random.Range(chestTypeSo.gemRange.min, chestTypeSo.gemRange.max);
        
        // Storing it as a string just to display it when chest spawns.
        coinsRange = $"{chestTypeSo.coinRange.min}-{chestTypeSo.coinRange.max}";
        gemsRange = $"{chestTypeSo.gemRange.min}-{chestTypeSo.gemRange.max}";
    }
}
