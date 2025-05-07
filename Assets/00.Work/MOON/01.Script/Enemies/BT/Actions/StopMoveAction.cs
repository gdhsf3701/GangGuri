using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _00.Work.MOON._01.Script.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "StopMove", story: "set [Movement] isStop to [NewValue]", category: "Enemy/Move", id: "42f79a69549dafef64d8aead38aacb11")]
    public partial class StopMoveAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyMovement> Movement;
        [SerializeReference] public BlackboardVariable<bool> NewValue;

        protected override Status OnStart()
        {
            Movement.Value.SetStop(NewValue.Value);
            return Status.Success;
        }
    }
}

