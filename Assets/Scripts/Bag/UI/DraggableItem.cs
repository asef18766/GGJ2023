using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using Web.UI;

namespace Bag.UI
{
    public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Vector3 _oriLoc;
        [SerializeField] private ItemID itemID;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Text itemCntText;
        [SerializeField] private int itemCnt;
        [SerializeField] private List<Sprite> itemIcons;

        [SerializeField] private float normalSeedSuccessRate = 0.5f;
        [SerializeField] private float errorSeedSuccessRate = 0.2f;
        [SerializeField] private float adSeedSuccessRate = 0.3f;
        [SerializeField] private float cloudMultChance = 2;
        [SerializeField] private float accTimeMultiplier = 2;
        private Action<PlantableArea> _useBehaviour;
        
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

        #region Seed_Functions

        private void NormalSeedUse(PlantableArea area)
        {
            area.MulChance(normalSeedSuccessRate);
            area.StartCoroutine("Sprout");
        }

        private void ErrorSeedUse(PlantableArea area)
        {
            area.AddChance(errorSeedSuccessRate);
            area.StartCoroutine("Sprout");
            area.StartCoroutine("PopRandomErrors");
        }

        private void AdSeedUse(PlantableArea area)
        {
            area.AddChance(adSeedSuccessRate);
            area.StartCoroutine("Sprout");
            area.StartCoroutine("PopRndAds");
        }

        private void AccChipUse(PlantableArea area) => area.MultTime(1 / accTimeMultiplier);
        private void CloudChipUse(PlantableArea area) => area.MulChance(cloudMultChance);
        private void ShadowChipUse(PlantableArea area) => area.Spread();
        #endregion
        

        public void InitProps(ItemID id, int cnt)
        {
            itemCntText.text = cnt.ToString();
            itemCnt = cnt;
            itemID = id;
            switch (itemID)
            {
                case ItemID.NormalSeed:
                    _useBehaviour = NormalSeedUse;
                    break;
                case ItemID.RGBSeed:
                    _useBehaviour = NormalSeedUse;
                    gameObject.GetComponent<GraphicAdaptor>().enabled = true;
                    break;
                case ItemID.ErrorSeed:
                    _useBehaviour = ErrorSeedUse;
                    break;
                case ItemID.ADSeed:
                    _useBehaviour = AdSeedUse;
                    break;
                case ItemID.AccChip:
                    _useBehaviour = AccChipUse;
                    break;
                case ItemID.CloudChip:
                    _useBehaviour = CloudChipUse;
                    break;
                case ItemID.ShadowChip:
                    _useBehaviour = ShadowChipUse;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            GetComponent<Image>().sprite = itemIcons[(int) id];
        }

        public void UseItem(PlantableArea area)
        {
            Debug.LogWarning($"use item {itemID}");
            _useBehaviour(area);
            itemCnt--;
            if (itemCnt == 0)
                Destroy(gameObject);
            itemCntText.text = itemCnt.ToString();
        }
    }
}