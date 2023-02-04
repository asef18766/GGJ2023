using System.Collections;
using System.Collections.Generic;
using Bag.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Web.UI
{
    public class PlantableArea : MonoBehaviour, IDropHandler
    {
        [SerializeField] private float chance = 0.6f;
        [SerializeField] private float duration = 3.0f;
        [SerializeField] private Sprite[] sprouteImgs;
        [SerializeField] private float spreadChance = 0.6f;
        [SerializeField] private Sprite failedImg;
        private Image _curImg;

        private bool PassStage()
        {
            if (chance >= 1)
                return true;
            Debug.Log($"cur success chance:{chance}");
            var failedRate = Mathf.Pow(1 - chance, 1f / 3f);
            Debug.Log($"failed rate: {failedRate}");
            var drawNum = Random.Range(0f, 1f);
            Debug.Log($"draw num: {drawNum}");
            return  failedRate <= drawNum;
        }

        private bool _isSprouting;

        public bool IsSprouting
        {
            get => _isSprouting;
        }

        private IEnumerator Sprout()
        {
            if (_isSprouting)
                yield break;
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            _isSprouting = true;
            // first stage
            _curImg.sprite = sprouteImgs[0];
            yield return new WaitForSeconds(duration / 3);
            
            
            // second stage
            if (PassStage())
                Debug.Log($"{gameObject.name} pass first stage");
            else
            {
                Debug.Log($"{gameObject.name} first stage failed");
                _curImg.sprite = failedImg;
                _popErrs = false;
                yield break;
            }
            _curImg.sprite = sprouteImgs[1];
            yield return new WaitForSeconds(duration / 3);
            
            // third stage
            if (PassStage())
                Debug.Log($"{gameObject.name} pass second stage");
            else
            {
                Debug.Log($"{gameObject.name} second stage failed");
                _curImg.sprite = failedImg;
                _popErrs = false;
                yield break;
            }
            _curImg.sprite = sprouteImgs[2];
            yield return new WaitForSeconds(duration / 3);


            // last stage
            if (PassStage())
                Debug.Log($"{gameObject.name} pass last stage");
            else
            {
                Debug.Log($"{gameObject.name} least stage failed");
                _curImg.sprite = failedImg;
                _popErrs = false;
                yield break;
            }
            _curImg.sprite = sprouteImgs[3];

        }
        public void AddChance(float val) => chance += val;
        public void MulChance(float val) => chance *= val;

        public void MultTime(float val) => duration *= val;

        public void OnDrop(PointerEventData eventData)
        {
            var item = eventData.pointerDrag.GetComponent<DraggableItem>();
            if (item == null) return;
            Debug.Log("trigger plantable area");
            item.UseItem(this);
            StartCoroutine(Sprout());
        }

        [SerializeField] private GameObject errWin;
        [SerializeField] private Transform errWinBg;
        private bool _popErrs;
        private readonly List<GameObject> _errWins = new();
        private IEnumerator PopRandomErrors()
        {
            _popErrs = true;
            var w = GetComponent<RectTransform>().rect.width;
            var h = GetComponent<RectTransform>().rect.height;
            for (var i = 0; i < 4; i++)
            {
                var go = Instantiate(errWin, errWinBg);
                go.GetComponent<RectTransform>().SetLocalPositionAndRotation(new Vector3(Random.Range(-w/2, w/2), Random.Range(-h/2, h/2)), Quaternion.identity);
                _errWins.Add(go);
            }

            while (_popErrs)
            {
                var rdVal = Random.Range(0, 16);
                //Debug.Log($"rdVal: {rdVal}");
                for (var i = 0; i < 4; i++)
                    _errWins[i].SetActive((rdVal & (1 << i)) != 0);
                yield return new WaitForSeconds(0.2f);
            }

            foreach (var go in _errWins)
                Destroy(go); 
        }

        [SerializeField] private GameObject adWin;
        private IEnumerator PopRndAds()
        {
            errWin = adWin;
            return PopRandomErrors();
        }

        public void Spread()
        {
            Debug.Log("try spreading");
            if (Random.Range(0f, 1f) >= spreadChance)
            {
                Debug.Log("spread failed :(");
            }

            foreach (Transform others in transform.parent)
            {
                var area = others.GetComponent<PlantableArea>();
                if (area == null || area == this) 
                    continue;
                if (area.IsSprouting)
                    continue;
                area.StartCoroutine("Sprout");
                Debug.LogWarning($"spread to {area.gameObject.name}");
            }

            StartCoroutine(Sprout());
        }

        private void Start()
        {
            _curImg = GetComponent<Image>();
            
            var bgTransform = errWinBg.GetComponent<RectTransform>();
            var rect = GetComponent<RectTransform>();
            var rect1 = rect.rect;
            bgTransform.sizeDelta = new Vector2(rect1.width, rect1.height);
            bgTransform.position = rect.position;
        }
    }
}