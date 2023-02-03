using System;
using UnityEngine;

namespace Bag.UI
{
    public class BagLoader : MonoBehaviour
    {
        [SerializeField] private Transform listTransform;
        [SerializeField] private GameObject draggableItem;
        private void OnEnable()
        {
            foreach (var (itemID, itemCnt) in Bag.Instance.Inventory)
            {
                var obj = Instantiate(draggableItem, listTransform);
                var dgi = obj.GetComponent<DraggableItem>();
                dgi.InitProps(itemID, itemCnt);
            }
        }

        private void OnDisable()
        {
            foreach (Transform child in listTransform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}