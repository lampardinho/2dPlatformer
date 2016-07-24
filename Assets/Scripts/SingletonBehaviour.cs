using UnityEngine;

namespace Assets.Scripts
{
    public class SingletonBehaviour<T> : MonoBehaviour where T: MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        Debug.LogError("No " + typeof(T) + " Singleton found");
                    }
                }
                return _instance;
            }
        }
    }
}
