using _00.Work.MOON._01.Script.Entities;
using Unity.Behavior;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Enemies
{
    public class Enemy : Entity
    {
        [field: SerializeField] public EntityFinderSO PlayerFinder { get; set; }
        public BehaviorGraphAgent BtAgent { get; private set; }
        
        
        protected override void AddComponents()
        {
            base.AddComponents();
            BtAgent = GetComponent<BehaviorGraphAgent>();
            Debug.Assert(BtAgent != null, $"{gameObject.name} don't have a BehaviorGraphAgent");
        }
        
        public BlackboardVariable<T> GetBlackboardVariable<T>(string key)
        {
            if(BtAgent.GetVariable(key, out BlackboardVariable<T> result))
            {
                return result;
            }
            return default;
        }
    }
}
