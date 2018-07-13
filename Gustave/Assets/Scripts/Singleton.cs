using UnityEngine;

namespace Helpers
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T GetInstance()
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }
}
