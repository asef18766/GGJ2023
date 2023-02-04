using System;
using Bag;
using UnityEngine;
using UnityEngine.UI;

namespace Money.UI
{
    public class BuyWidget : MonoBehaviour
    {
        [SerializeField] private ItemID itemID;
        public void Start()
        {
            foreach (Transform tf in transform)
            {
                var priceDisplay = tf.gameObject.GetComponent<PriceDisplay>();
                if (priceDisplay == null) continue;
                itemID = priceDisplay.GetItemID();
                Debug.Log($"{gameObject.name} with itemID {itemID}");
                break;
            }
            GetComponent<Button>().onClick.AddListener(Buy);
        }

        private void Buy()
        {
            Debug.Log($"buying {itemID}");
            var ins = MoneyManager.Instance;
            ins.Spend(ins.GetItemPrice(itemID));
            Bag.Bag.Instance.AddItem((int) itemID);
        }
    }
}