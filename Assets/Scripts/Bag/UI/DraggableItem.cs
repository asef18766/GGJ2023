using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Bag.UI
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _oriLoc;
        [SerializeField] private int itemID;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Text itemCntText;
        [SerializeField] private int itemCnt;

        #region UI_Method
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("begin drag");
            _oriLoc = transform.position;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("end drag");
            transform.position = _oriLoc;
            _canvasGroup.blocksRaycasts = true;
        }
        #endregion
        
        public void InitProps(int id, int cnt)
        {
            itemCntText.text = cnt.ToString();
            itemCnt = cnt;
            itemID = id;
        }

        public void UseItem()
        {
            Debug.LogWarning($"use item {itemID}");
            itemCnt--;
            if (itemCnt == 0)
                Destroy(gameObject);
            itemCntText.text = itemCnt.ToString();
        }
    }
}