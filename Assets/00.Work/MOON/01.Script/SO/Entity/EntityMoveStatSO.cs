using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Entity
{
    [CreateAssetMenu(fileName = "Move", menuName = "SO/Entity/MoveStat", order = 0)]
    public class EntityMoveStatSO : ScriptableObject
    {
        
        
        [field: SerializeField]
        public float SlopeSpeed { get; private set; } = 15f;
        
        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 25f;
        
        [field: SerializeField]
        public float MaxAngle { get; private set; } = 4f;
        
        [field: SerializeField]
        public float MaxSpeed { get; private set; } = 50f;
    }
}