using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Entity
{
    [CreateAssetMenu(fileName = "Move", menuName = "SO/Entity/MoveStat", order = 0)]
    public class EntityMoveStatSO : ScriptableObject
    {
        
        
        [field: SerializeField]
        public float MoveToUpSpeed { get; private set; } = 15f;
        
        [field: SerializeField]
        public float MoveSpeed { get; private set; } = 25f;
        
        [field: SerializeField]
        public float UpDistance { get; private set; } = 1f;
        
        [field: SerializeField]
        public float MaxSpeed { get; private set; } = 50f;
        
        [field: SerializeField]
        public float JumpPower { get; private set; } = 10f;
        
        [field: SerializeField]
        public float RotateSpeed { get; private set; } = 5f;

        [field: SerializeField] 
        public float MaxSlopeAngle { get; private set; } = 45f;
        [field: SerializeField]
        public float StopPer { get; private set; } = 0.15f;
        [field: SerializeField]
        public float StopDistance { get; private set; } = 1.35f;
        [field: SerializeField]
        public GameObject CarPrefab { get; private set; }
    }
}