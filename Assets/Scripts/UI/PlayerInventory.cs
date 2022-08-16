using Singleton;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerInventory : MonoGenericSingleton<PlayerInventory>
    {
        [SerializeField] private int coins;
        [SerializeField] private int gems;
        [SerializeField] private Text coinTxt;
        [SerializeField] private Text gemsTxt;
        private bool hasEnoughGems;

        private void Start()
        {
            DisplayPlayerInventory();
        }

        private void DisplayPlayerInventory()
        {
            coinTxt.text = coins.ToString();
            gemsTxt.text = gems.ToString();
        }

        public void UpdatePlayerInventory(int coins, int gems)
        {
            this.coins += coins;
            this.gems += gems;
            DisplayPlayerInventory();
        }
        
        public bool DeductGems(int gems)
        {
            if (gems <= this.gems)
            {
                this.gems -= gems;
                DisplayPlayerInventory();
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