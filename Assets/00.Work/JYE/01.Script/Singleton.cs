using UnityEngine;

namespace _00.Work.JYE._01.Script
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = (T)FindAnyObjectByType(typeof(T));

                    if(_instance == null)
                    {
                        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                        _instance = obj.GetComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }
}