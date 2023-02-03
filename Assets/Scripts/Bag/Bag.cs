using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;
namespace Bag
{
    public enum ItemID
    {
        Meow = 87,
        Wolf
    }
    [CreateAssetMenu(menuName = "Bag")]
    public class Bag : ScriptableObjectSingleton<Bag>
    {
        public readonly Dictionary<int, int> Inventory = new();
        
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