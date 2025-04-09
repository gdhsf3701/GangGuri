using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Entity
{
    public abstract class EntityMoveStatInfoSO : ScriptableObject
    {
        [field: SerializeField]
        [SerializedDictionary("MoveType","MoveSO")] 
        public SerializedDictionary<EntityMoveStatType,EntityMoveStatSO> MoveStats { get; private set; } = new();
        
    }

    public enum EntityMoveStatType
    {
        Normal,
        Slow,
        Fast,
    }
}