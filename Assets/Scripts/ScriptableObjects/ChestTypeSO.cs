using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    [System.Serializable]
    public struct IntegerRange
    {
        public int min;
        public int max;
    }
    
    [CreateAssetMenu(fileName = "Chest Type", menuName = "Scriptable Objects/Chest/New chest type")]
    public class ChestTypeSo : ScriptableObject
    {
        public ChestTypes chestType;
        
        [Tooltip("Time(secs) needed to unlock this chest.")]
        public int unlockTime;
        public int gemsRequiredToUnlock;
        
        [Header("Rewards")]
        [Tooltip("Minimum and Maximum coins that user can get awarded with this chest.")]
        public IntegerRange coinRange;
        
        [Tooltip("Minimum and Maximum gems that user can get awarded with this chest.")]
        public IntegerRange gemRange;
        
        [Header("Images")]
        public Sprite lockedChestSprite;
        public Sprite unlockedChestSprite;
    }
}