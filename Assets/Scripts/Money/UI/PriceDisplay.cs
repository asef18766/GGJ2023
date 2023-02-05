using System;
using Bag;
using UnityEngine;
using UnityEngine.UI;

namespace Money.UI
{
    public class PriceDisplay : MonoBehaviour
    {
        [SerializeField] private Text num;
        [SerializeField] private ItemID itemID;
        private void OnEnable()
        {
            num.text = MoneyManager.Instance.GetItemPrice(itemID).ToString();
        }

        public ItemID GetItemID() => itemID;
    }
}