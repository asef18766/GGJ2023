using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Utils
{
    public class GraphicAdaptor : MonoBehaviour
    {
        [SerializeField] private bool goodGraphicCard; 
        [SerializeField] private List<Color> cmyk;
        private Image _baseImg;
        private void Start()
        {
            //Debug.LogWarning($"{gameObject.name} start !!");
            _baseImg = GetComponent<Image>();                                                                                                                                                                                                                                                                                                                                                                                                                          
            
            goodGraphicCard = SystemInfo.graphicsDeviceName.Contains("60") ||
                              SystemInfo.graphicsDeviceName.Contains("70") ||
                              SystemInfo.graphicsDeviceName.Contains("80");
            
            StartCoroutine(goodGraphicCard ? GoodGraphicCardEffect() : BadGraphicCardEffect());
        }

        private static float _getNxtVal(float val)
        {
            var delta = Random.Range(0f, 1f) * 0.01f;
            return (val + delta > 1) ? val + delta - 1 : val + delta;
        }

        private IEnumerator GoodGraphicCardEffect()
        {
            while (true)
            {
                var rnd = Random.Range(0, 8);
                if (_baseImg == null)
                {
                    Debug.LogWarning("detect null instance");
                    break;
                }

                var curR = _getNxtVal(_baseImg.color.r);
                var curG = _getNxtVal(_baseImg.color.g);
                var curB = _getNxtVal(_baseImg.color.b);
                //Debug.Log($"r: {curR} g:{curG} b:{curB}");
                _baseImg.color = new Color(curR, curG, curB, 255);
                yield return new WaitForSeconds(0.01f);
            }
        }

        private IEnumerator BadGraphicCardEffect()
        {
            var idx = 0;
            while (true)
            {
                _baseImg.color = cmyk[idx % cmyk.Count];
                yield return new WaitForSeconds(1);
                idx++;
            }
        }
    }
}