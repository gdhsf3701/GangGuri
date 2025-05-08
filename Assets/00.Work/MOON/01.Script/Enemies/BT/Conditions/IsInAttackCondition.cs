using System;
using Unity.Behavior;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Enemies.BT.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "IsInAttack", story: "[Target] is In [Self] attackRange", category: "Conditions", id: "b3eb410aa679e7777a2cc8639930845e")]
    public partial class IsInAttackCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<Enemy> Self;

        public override bool IsTrue()
        {
            float distance = Vector3.Distance(Target.Value.position, Self.Value.transform.position);
            return distance < Self.Value.attackRange;
        }
    }
}
