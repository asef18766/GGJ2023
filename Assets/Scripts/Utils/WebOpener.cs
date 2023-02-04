using UnityEngine;

namespace Utils
{
    public class WebOpener : MonoBehaviour
    {
        public void OpenUrl(string url)
        { 
            Application.OpenURL(url);
        }
    }
}