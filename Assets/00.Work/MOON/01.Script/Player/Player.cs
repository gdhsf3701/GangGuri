using _00.Work.MOON._01.Script.SO;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Player
{
    public class Player : Entity
    {
        [SerializeField] InputReader input;
    
        protected override void Awake()
        {
            base.Awake();
            input.OnMoveEvent += GetCompo<PlayerMovement>().MoveDirChanged;
            input.OnJumpEvent += GetCompo<PlayerMovement>().Jump;
        }
    }
}
