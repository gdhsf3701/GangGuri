using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public class GroundChecker : MonoBehaviour , IEntityComponent
    {
        [SerializeField] Vector3 checkerSize;
        [SerializeField] Vector3 checkerTransSet;
        [SerializeField] LayerMask groundLayer;
        Entities.Entity _entity;

        public bool GroundCheck()
        {
            return Physics.CheckBox(transform.position + checkerTransSet, checkerSize, _entity.transform.rotation,
                groundLayer);
        }

        public void Initialize(Entities.Entity entity)
        {
            _entity = entity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position + checkerTransSet, checkerSize);
            Gizmos.color = Color.white;
        }
    }
}