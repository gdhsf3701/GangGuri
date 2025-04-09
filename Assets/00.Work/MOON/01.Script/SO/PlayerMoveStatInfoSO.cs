using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.MOON._01.Script.SO
{
    [CreateAssetMenu(fileName = "PlayerMoveStatInfo", menuName = "SO/Player/MoveStatInfo", order = 0)]
    public class PlayerMoveStatInfoSO : ScriptableObject
    {
        [field: SerializeField]
        public PlayerMoveStatSO[] MoveStats { get; private set; }
        
        [field: SerializeField]
        public PlayerInputSO PlayerInput { get; private set; }
        
        [field: SerializeField]
        public Dictionary<string,string> NormalStatName { get; private set; }
    }
}