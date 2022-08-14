using Singleton;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerInventory : MonoGenericSingleton<PlayerInventory>
    {
        [SerializeField] private int _coins;
        [SerializeField] private int _gems;
        [SerializeField] private TextMeshProUGUI coinTxt;
        [SerializeField] private TextMeshProUGUI gemsTxt;
        private bool hasEnoughGems;

        public void ShowPlayerInventory()
        {
            coinTxt.text = _coins.ToString();
            gemsTxt.text = _gems.ToString();
        }
        public bool RemoveGems(int gems)
        {
            if (gems <= _gems)
            {
                _gems -= gems;
                ShowPlayerInventory();
                return hasEnoughGems = true;
            }
            return hasEnoughGems = false;
        }
    }
}