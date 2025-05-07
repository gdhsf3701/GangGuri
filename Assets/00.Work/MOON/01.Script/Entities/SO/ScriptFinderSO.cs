using System;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities.SO
{
    public abstract class ScriptFinderSO<T> : ScriptableObject where T : MonoBehaviour
    {
        public Type KeyType = typeof(T);
        
        public T Target { get; private set; }

        public void SetTarget(T go)
        {
            Target = go;
        }
    }
}
