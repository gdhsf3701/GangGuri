using System;
using _00.Work.MOON._01.Script.Core.DI;
using _00.Work.MOON._01.Script.Entities.SO;
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
                Type keyType = finder.KeyType;
                SerializableType serializableKey = keyType;
            
                if (_components.TryGetValue(serializableKey, out MonoBehaviour component))
                {
                    finder.SetTarget(component);
                }
                else
                {
                    Debug.LogWarning($"Component for type {keyType} not found in _components dictionary.");
                }
            }
        }
        public MonoScript GetMonoScriptForType(Type keyType)
        {
            string[] guids = AssetDatabase.FindAssets("t:MonoScript");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                if (script != null && script.GetClass() == keyType)
                    return script;
            }
            Debug.LogError($"MonoScript not found for type {keyType.FullName}");
            return null;
        }
    }
}