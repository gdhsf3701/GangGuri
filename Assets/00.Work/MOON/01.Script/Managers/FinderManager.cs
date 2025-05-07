using System;
using System.Collections.Generic;
using _00.Work.MOON._01.Script.Core.DI;
using _00.Work.MOON._01.Script.Entities;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Managers
{
    [DefaultExecutionOrder(-1)]
    public class FinderManager : MonoBehaviour
    {
        [Inject,SerializeField] private SerializedDictionary<MonoBehaviour , Type> injects;
        //[SerializeField] private ScriptFinderSOBase[] monoChecks;
        //private Dictionary<MonoBehaviour, ScriptFinderSOBase> finder = new();
        
        private void Awake()
        {
            // foreach (ScriptFinderSOBase monoCheck in monoChecks)
            // {
            //     finder.Add(monoCheck.KeyType, monoCheck);
            // }
            // playerFinder.SetTarget(player);
            // enemyManagerFinder.SetTarget(enemyManager);
        }
    }
}