using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _00.Work.MOON._01.Script.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "MoveToTarget", story: "[Movement] move to [Target]", category: "Enemy/Move", id: "e7c880290f746e4d8481a6e9c5f7433f")]
    public partial class MoveToTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<EnemyMovement> Movement;
        [SerializeReference] public BlackboardVariable<Transform> Target;

        protected override Status OnStart()
        {
            Movement.Value.SetDestination(Target.Value.position);
            return Status.Success;
        }
    }
}

