using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Entity
{
    [CreateAssetMenu(fileName = "MovePer", menuName = "SO/Entity/MoveStatPer", order = 0)]
    public class EntityMoveStatPerSO : ScriptableObject
    {
        [field: Header("EntityMoveStatPerSO")]
        [field: SerializeField]
        public float MoveSpeedPer { get; private set; } = 1;
        [field: SerializeField]
        public float RotateSpeedPer { get; private set; } = 1;
    }
}