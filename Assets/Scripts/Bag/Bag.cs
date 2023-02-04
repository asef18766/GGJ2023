using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
namespace Bag
{
    public enum ItemID
    {
        NormalSeed = 0,
        RGBSeed,
        ErrorSeed,
        ADSeed,
        AccChip,
        CloudChip,
        ShadowChip
    }
    [CreateAssetMenu(menuName = "Bag")]
    public class Bag : ScriptableObjectSingleton<Bag>
    {
        public readonly Dictionary<int, int> Inventory = new();

        private void OnEnable()
        {
            for (var i = 0; i <= (int) ItemID.ShadowChip; ++i)
            {
                Inventory.Add(i, 99);
            }
        }

        public void AddItem(int id)
        {
            if(!Inventory.ContainsKey(id))
                Inventory.Add(id, 0);
            Inventory[id]++;
        }

        public void RemoveItem(int id)
        {
            if (!Inventory.ContainsKey(id))
                throw new IndexOutOfRangeException($"inventory does not exist");
            Inventory[id]--;
            if (Inventory[id] == 0)
                Inventory.Remove(id);
        }

        public void Clear()
        {
            Inventory.Clear();
        }
    }
}