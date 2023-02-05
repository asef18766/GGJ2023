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
                if (_sInstance == null);
                    _sInstance = Resources.Load<T>(typeof(T).Name);
                return _sInstance;
            }
        }
    }
}