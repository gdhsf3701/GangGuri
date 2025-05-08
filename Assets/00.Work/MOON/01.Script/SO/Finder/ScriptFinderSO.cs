using System;
using _00.Work.MOON._01.Script.Managers;
using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Finder
{
    public abstract class ScriptFinderSOBase : ScriptableObject
    {
        public abstract SerializableType KeyType { get; }
        public abstract void SetTarget(MonoBehaviour target);
    }

    public abstract class ScriptFinderSO<T> : ScriptFinderSOBase where T : MonoBehaviour
    {
        public override SerializableType KeyType => typeof(T);
        public T Target { get; private set; }

        public override void SetTarget(MonoBehaviour target)
        {
            Target = target as T;
        }
    }
}
