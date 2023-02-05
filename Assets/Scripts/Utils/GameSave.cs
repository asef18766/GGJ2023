using UnityEngine;

namespace Utils
{
    [CreateAssetMenu(fileName = "GameSave", menuName = "GameSave")]
    public class GameSave : ScriptableObjectSingleton<GameSave>
    {
        public bool isPassed;
    }
}