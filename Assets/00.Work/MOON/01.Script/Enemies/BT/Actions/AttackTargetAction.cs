using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _00.Work.MOON._01.Script.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "AttackTarget", story: "[Self] attack targets", category: "Action", id: "a600a22cce231a3fbb8bb04118ff4634")]
    public partial class AttackTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Enemy> Self;

        protected override Status OnStart()
        {
            return Status.Success;
        }
    }
}

