using System;
using Singleton;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PlayerInventory : MonoGenericSingleton<PlayerInventory>
    {
        [SerializeField] private int _coins;
        [SerializeField] private int _gems;
        [SerializeField] private Text coinTxt;
        [SerializeField] private Text gemsTxt;
        private bool hasEnoughGems;

        private void Start()
        {
            ShowPlayerInventory();
        }

        public void ShowPlayerInventory()
        {
            coinTxt.text = _coins.ToString();
            gemsTxt.text = _gems.ToString();
        }

        public void UpdatePlayerInventory(int coins, int gems)
        {
            _coins = coins;
            _gems = gems;
            ShowPlayerInventory();
        }
        public bool RemoveGems(int gems)
        {
            if (gems <= _gems)
            {
                _gems -= gems;
                ShowPlayerInventory();
                hasEnoughGems = true;
            }
            else
            {
                hasEnoughGems = false;
            }
            
            return hasEnoughGems;
        }
    }
}