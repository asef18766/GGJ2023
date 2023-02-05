using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Web.UI;

namespace Web
{
    public class FinalChecker : MonoBehaviour
    {
        [SerializeField] private PlantableAreaDetector pd;

        private void Start()
        {
            pd.successCallbacks.Add(() =>
            {
                Debug.LogWarning("game passed!!");
                GameSave.Instance.isPassed = true;
                SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
            });
        }

        private void Update()
        {
            if (pd.GetAnyFailed())
                SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
        }
    }
}