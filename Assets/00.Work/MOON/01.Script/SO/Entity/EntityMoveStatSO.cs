using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Entity
{
    public class EntityMoveStatSO : ScriptableObject
    {
        [field: Header("EntityMoveStat")]
        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 5f;
        
        [field: SerializeField]
        public float RotateSpeed { get; private set; } = 1.5f;

        [field: SerializeField] 
        public float MaxSlopeAngle { get; private set; } = 45f;
        
        [field: SerializeField]
        public float StopDistance { get; private set; } = 1.35f;
    }
}