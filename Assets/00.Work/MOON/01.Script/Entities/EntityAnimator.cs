using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public class EntityAnimator : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private Animator animator;

        private Entities.Entity _entity;

        public void Initialize(Entities.Entity entity)
        {
            _entity = entity;
        }

        public void SetParam(int hash, float value) => animator.SetFloat(hash, value);
        public void SetParam(int hash, int value) => animator.SetInteger(hash, value);
        public void SetParam(int hash, bool value) => animator.SetBool(hash, value);
        public void SetParam(int hash) => animator.SetTrigger(hash);
    }
}