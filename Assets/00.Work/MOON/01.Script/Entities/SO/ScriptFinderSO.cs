using System;
using UnityEditor;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities.SO
{
    public abstract class ScriptFinderSOBase : ScriptableObject
    {
        public abstract Type KeyType { get; }
        public abstract void SetTarget(MonoBehaviour target);
    }

    public abstract class ScriptFinderSO<T> : ScriptFinderSOBase where T : MonoBehaviour
    {
        public override Type KeyType => typeof(T);
        public T Target { get; private set; }

        public override void SetTarget(MonoBehaviour target)
        {
            Target = target as T;
        }
    }
}
