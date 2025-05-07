using System;
using Unity.Behavior;
using Unity.Mathematics;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

namespace _00.Work.MOON._01.Script.Enemies.BT.Actions
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "RotateToTarget", story: "[Self] rotate to [Taret] in [Second]", category: "Enemy/Move", id: "5fa876b2897d4e54be2d683a460db0c6")]
    public partial class RotateToTargetAction : Action
    {
        [SerializeReference] public BlackboardVariable<Enemy> Self;
        [SerializeReference] public BlackboardVariable<Transform> Taret;
        [SerializeReference] public BlackboardVariable<float> Second;
        
        
        private float _startTime;
        protected override Status OnStart()
        {
            _startTime = Time.time;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            LookTargetSmoothly();
            if(Time.time - _startTime >= Second.Value)
                return Status.Success;
            return Status.Running;
        }

        private void LookTargetSmoothly()
        {
            const float rotationSpeed = 10f;
            Vector3 direction = Taret.Value.position - Self.Value.transform.position;
            direction.y = 0;
            quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
            Quaternion rotation = Quaternion.Slerp(
                Self.Value.transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed);
            
            Self.Value.transform.rotation = rotation;
        }

        protected override void OnEnd()
        {
        }
    }
}

