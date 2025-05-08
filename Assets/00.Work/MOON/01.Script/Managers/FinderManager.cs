using System;
using _00.Work.MOON._01.Script.Core.DI;
using _00.Work.MOON._01.Script.SO.Finder;
using AYellowpaper.SerializedCollections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Managers
{
    [DefaultExecutionOrder(-1)]
    public class FinderManager : MonoBehaviour
    {
        [SerializeField,Inject] private SerializedDictionary<SerializableType, MonoBehaviour> _components;
        [SerializeField] private ScriptFinderSOBase[] finders;

        private void Awake()
        {
            foreach (ScriptFinderSOBase finder in finders)
            {
                SerializableType serializableKey = finder.KeyType;
                if (_components.TryGetValue(serializableKey, out MonoBehaviour component))
                {
                    finder.SetTarget(component);
                }
                else
                {
                    Debug.LogWarning($"Component for type {serializableKey.Type} not found in _components dictionary.");
                }
            }
        }
    }
}