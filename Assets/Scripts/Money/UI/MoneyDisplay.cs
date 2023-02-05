using System;
using UnityEngine;
using UnityEngine.UI;

namespace Money.UI
{
    public class MoneyDisplay : MonoBehaviour
    {
        [SerializeField] private Text num;

        private void Update()
        {
            num.text = MoneyManager.Instance.GetMoney().ToString();
        }
    }
}