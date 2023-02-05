using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneChanger : MonoBehaviour
    {
        public void LoadSceneName(string str)
        {
            SceneManager.LoadScene(str, LoadSceneMode.Single);
        }
    }
}