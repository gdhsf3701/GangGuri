using UnityEngine;

namespace _00.Work.MOON._01.Script.SO
{
    [CreateAssetMenu(fileName = "PlayerMoveStat", menuName = "SO/Player/MoveStat", order = 0)]
    public class PlayerMoveStatSO : ScriptableObject
    {
        private string _statName;

        public bool CheckStatName(string statName)
        {
            if(this == null)
                return false;
            return _statName.Equals(statName);
        }
        
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