using Project.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerInventory : MonoGenericSingleton<PlayerInventory>
    {
        [SerializeField] private int coinsInInventory;
        [SerializeField] private int gemsInInventory;
        [SerializeField] private Text coinTxt;
        [SerializeField] private Text gemsTxt;

        private void Start()
        {
            DisplayPlayerInventory();
        }

        private void DisplayPlayerInventory()
        {
            coinTxt.text = coinsInInventory.ToString();
            gemsTxt.text = gemsInInventory.ToString();
        }

        public void UpdatePlayerInventory(int coins, int gems)
        {
            this.coinsInInventory += coins;
            gemsInInventory += gems;
            DisplayPlayerInventory();
        }
        
        public bool DeductGems(int requiredGems)
        {
            if (requiredGems <= gemsInInventory)
            {
                gemsInInventory -= requiredGems;
                DisplayPlayerInventory();
                return true;
            }
            return false;
        }
    }
}