using UnityEngine;

namespace _00.Work.MOON._01.Script.SO.Cam
{
    [CreateAssetMenu(fileName = "CamSetting", menuName = "SO/CamSetting")]
    public class CamSettingSO : ScriptableObject
    {
        [field: SerializeField] 
        public float ResetTime { get; private set; } = 1.5f;
        
        [field: SerializeField]
        public float TurnSpeedX { get; private set; } = 5;
    
        [field: SerializeField]
        public float TurnSpeedY { get; private set; } = 2.5f;
        
        [field: SerializeField] 
        public float GoDefaultRotateTime { get; private set; } = 2f;
        
        
        [field: SerializeField]
        public Vector2 XMinMax {get; private set;} = new Vector2(-53f, 72f);
    
        [field: SerializeField]
        public Vector2 CamDistanceMinMax {get; private set;} = new Vector2(0.01f, 10f);
        
        [field:SerializeField]
        public Vector3 CamDeffultRotate { get; private set; }
    }
}
