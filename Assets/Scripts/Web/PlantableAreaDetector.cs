using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Web.UI;

namespace Web
{
    public class PlantableAreaDetector : MonoBehaviour
    {
        private bool CheckAll(int val) => transform.Cast<Transform>().All(tf => tf.gameObject.GetComponent<PlantableArea>().stage == val);
        private bool CheckAny(int val) => transform.Cast<Transform>().Any(tf => tf.gameObject.GetComponent<PlantableArea>().stage == val);

        private void Update()
        {
            if (CheckAny(1))
                Debug.LogWarning("can enter next stage");
            else if (CheckAll(-1))
                Debug.LogWarning("GG");
        }
    }
}