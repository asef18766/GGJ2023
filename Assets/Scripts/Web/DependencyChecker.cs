using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Web.UI;

namespace Web
{
    public class DependencyChecker : MonoBehaviour
    {
        [SerializeField] private List<PlantableAreaDetector> detectors;
        
        private bool[] _cklist;
        private void Start()
        {
            GetComponent<Button>().interactable = false;
            GetComponent<Image>().color = Color.red;
            _cklist = new bool[detectors.Count];
            var line = Resources.Load("Line") as GameObject;
            for(var i = 0; i != detectors.Count; ++i)
            {
                var i1 = i;
                detectors[i].successCallbacks.Add(() =>
                {
                    _cklist[i1] = true;
                    if (!_cklist.All(b => b)) return;
                    GetComponent<Button>().interactable = true;
                    GetComponent<Image>().color = Color.white;
                });

            }
        }
    }
}