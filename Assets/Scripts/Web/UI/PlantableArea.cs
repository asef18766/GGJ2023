using Bag.UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Web.UI
{
    public class PlantableArea : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("trigger plantable area");
            var item = eventData.pointerDrag.GetComponent<DraggableItem>();
            item.UseItem();
        }
    }
}