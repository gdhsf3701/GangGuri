using _00.Work.MOON._01.Script.SO.Entity;
using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Player
{
    [CreateAssetMenu(fileName = "PlayerMove", menuName = "SO/Player/MoveStat", order = 0)]
    public class PlayerMoveStatSO : EntityMoveStatSO
    {
        [field: Header("PlayerMoveStatSO")]
        [field: SerializeField]
        public float MoveToUpSpeed { get; private set; } = 1f;
        [field: SerializeField]
        public float UpDistance { get; private set; } = 2f;
        [field: SerializeField]
        public float JumpPower { get; private set; } = 10f;
        [field: SerializeField]
        public float MaxSpeed { get; private set; } = 50f;
        [field: SerializeField]
        public float StopPer { get; private set; } = 0.15f;

        [field: SerializeField] 
        public float UpDistanceCheckerSize { get; private set; } = 0.15f;
        [field: SerializeField]
        public GameObject CarPrefab { get; private set; }
    }
}