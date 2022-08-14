using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "ChestTypeList", menuName = "Scriptable Objects/Chest/New chestTypeList")]
    public class ChestTypeSoList : ScriptableObject
    {
        public ChestTypeSo[] chestsTypeList;
    }
}