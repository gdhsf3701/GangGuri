using System;
using Unity.Behavior;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Enemies.BT.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "CheckTargetFound", story: "[Self] check target found", category: "Conditions", id: "0e6df536a2e0956e6d828548d094a9d0")]
    public partial class CheckTargetFoundCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Enemy> Self;

        public override bool IsTrue()
        {
            return true;
        }
    }
}
