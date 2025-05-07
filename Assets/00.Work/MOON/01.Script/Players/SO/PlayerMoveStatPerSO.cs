using _00.Work.MOON._01.Script.SO.Entity;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players.SO
{
    [CreateAssetMenu(fileName = "MovePer", menuName = "SO/Player/MoveStatPer", order = 0)]
    public class PlayerMoveStatPerSO : EntityMoveStatPerSO
    {
        [field: Header("PlayerMoveStatPerSO")]
        [field: SerializeField]
        public float JumpPowerPer { get; private set; } = 1;
    }
}