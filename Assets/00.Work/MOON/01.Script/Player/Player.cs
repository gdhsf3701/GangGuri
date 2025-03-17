using UnityEngine;

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
