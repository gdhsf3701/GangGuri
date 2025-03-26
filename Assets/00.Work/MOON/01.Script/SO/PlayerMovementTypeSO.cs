using UnityEngine;

namespace _00.Work.MOON._01.Script.SO
{
    [CreateAssetMenu(fileName = "PlayerMovementType", menuName = "SO/Player/MovementType", order = 0)]
    public class PlayerMovementTypeSO : ScriptableObject
    {
        [field:SerializeField]public float moveSpeed { get; private set; }
        [field:SerializeField]public float jumpPower { get; private set; }
        [field:SerializeField]public float maxSpeed { get; private set; }
        [field:SerializeField]public float timer {get; private set;}
        [field:SerializeField]public MovementType Type { get; private set; }
    }

    public enum MovementType
    {
        Normal,
        Shock,
        Slow1,
        Slow2
    }
}