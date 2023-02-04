using System.Collections.Generic;
using Bag;
using UnityEngine;
using Utils;

namespace Money
{
    [CreateAssetMenu(fileName = "MoneyManager")]
    public class MoneyManager : ScriptableObjectSingleton<MoneyManager>
    {
        [SerializeField] private int money;
        [SerializeField] private List<int> itemPrice;

        public int GetItemPrice(ItemID itemID) => itemPrice[(int) itemID];
        public void Earn(int val) => money += val;
        public int GetMoney() => money;
        public bool Spend(int val)
        {
            if (money < val)
                return false;
            money -= val;
            return true;
        }
    }
}