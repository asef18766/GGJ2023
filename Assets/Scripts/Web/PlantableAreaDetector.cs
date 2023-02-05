using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Web.UI;

namespace Web
{
    public class PlantableAreaDetector : MonoBehaviour
    {
        private bool CheckAll(int val) => transform.Cast<Transform>().All(tf => tf.gameObject.GetComponent<PlantableArea>().stage == val);
        private bool CheckAny(int val) => transform.Cast<Transform>().Any(tf => tf.gameObject.GetComponent<PlantableArea>().stage == val);

        public List<Action> successCallbacks = new();

        public bool GetAnyFailed() => CheckAny(-1);
        private void Update()
        {
            if (CheckAny(1))
            {
                foreach (var cb in successCallbacks)
                    cb();

                enabled = false;
            }
            else if (CheckAll(-1))
            {
                Debug.LogWarning("GG");
                SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
            }
        }
    }
}