using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.Players;
using _00.Work.MOON._01.Script.SO.Finder;
using Unity.Behavior;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Enemies
{
    public abstract class Enemy : Entity
    {
        [field: SerializeField] public ScriptFinderSO<Player> PlayerFinder { get; protected set; }
        [field: SerializeField] public ScriptFinderSO<EnemyManager> EnemyManagerFinder { get; protected set; }
        public BehaviorGraphAgent BtAgent { get; private set; }

        #region Temp
        public float attackRange = 2f;
        [SerializeField]private LayerMask targetLayer;
        #endregion

        private void OnDrawGizmosSelected()
        {
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

        public void Attack()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
            if(hitColliders.Length > 0)
            {
                foreach (Collider hit in hitColliders)
                {
                    if (hit.transform.TryGetComponent(out IHitable hitable))
                    {
                        hitable.Hit(this);
                    }
                }
            }
        }
    }
}