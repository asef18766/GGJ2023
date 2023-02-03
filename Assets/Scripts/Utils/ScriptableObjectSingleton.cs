using UnityEditor;
using UnityEngine;

namespace Utils
{
    public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _sInstance;
        public static T Instance
        {
            get
            {
                if (_sInstance != null) return _sInstance;
                var findAssets = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
                if (findAssets == null || findAssets.Length == 0)
                {
                    Debug.LogError($"Please create ScriptableObject typeof {typeof(T)} first...");
                }
                else if (findAssets.Length > 1)
                {
                    Debug.LogError($"ScriptableObject typeof {typeof(T)} exist multiple，please check they...");
                }
                else
                {
                    _sInstance = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(findAssets[0]));
                }
                return _sInstance;
            }
        }
    }
}