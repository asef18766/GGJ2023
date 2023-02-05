using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Init
{
    public class EndSceneInit : MonoBehaviour
    {
        [SerializeField] private GameObject warnWin;
        [SerializeField] private Button restartBtn;
        private void Start()
        {
            if (!GameSave.Instance.isPassed)
                warnWin.SetActive(true);
            else
            {
                Debug.LogWarning("create save");
                PlayerPrefs.SetInt("passed", 87);
                PlayerPrefs.Save();
                restartBtn.onClick.RemoveAllListeners();
                restartBtn.onClick.AddListener(Application.Quit);
#if UNITY_EDITOR
                restartBtn.onClick.AddListener(() =>
                {
                    EditorApplication.isPlaying = false;
                });
#endif
            }
        }
    }
}