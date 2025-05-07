using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.Entities.SO;
using _00.Work.MOON._01.Script.Players;
using Unity.Behavior;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Enemies
{
    public abstract class Enemy : Entity
    {
        [field: SerializeField] public ScriptFinderSO<Player> PlayerFinder { get; set; }
        public BehaviorGraphAgent BtAgent { get; private set; }

        #region Temp

        public float detectRange = 8f;
        public float attackRange = 2f;

        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,attackRange);
        }

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