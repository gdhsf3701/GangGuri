using _00.Work.MOON._01.Script.SO.Entity;
using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Player
{
    [CreateAssetMenu(fileName = "PlayerMoveInfo", menuName = "SO/Player/MoveInfo", order = 0)]
    public class PlayerMoveStatInfoSO : EntityMoveStatInfoSO
    {
        [field: SerializeField]
        public PlayerInputSO PlayerInput { get; private set; }
    }
}