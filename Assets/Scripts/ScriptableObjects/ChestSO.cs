using Enums;
using UnityEngine;

namespace ScriptableObjects
{
    public class ChestSO : ScriptableObject
    {
        public ChestTypes chestType;
        public int unlockTime;
        public int minCoins;
        public int maxCoins;
        public int minGems;
        public int maxGems;
    }
}