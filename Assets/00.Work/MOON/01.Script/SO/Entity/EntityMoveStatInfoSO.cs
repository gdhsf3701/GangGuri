using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Entity
{
    public abstract class EntityMoveStatInfoSO : ScriptableObject
    {
        [field: SerializeField,SerializedDictionary("StatName","MoveStatSO")]
        public SerializedDictionary<string,EntityMoveStatSO> MoveStats { get; private set; } = new();
        
        [field: SerializeField,SerializedDictionary("StatName","MoveStatPerSO")]
        public SerializedDictionary<string,EntityMoveStatPerSO> MoveStatsPer { get; private set; } = new();
        
    }
}