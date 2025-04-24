using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public class GroundChecker : MonoBehaviour , IEntityComponent
    {
        [SerializeField] float checkerDistance;
        [SerializeField] LayerMask groundLayer;
        
        #region testEntityGizmos
        [SerializeField]private Entity _entity;
        [SerializeField] private Color checkerColor;
        [SerializeField] private Mesh box;
        #endregion
        
        public bool GroundCheck(out RaycastHit hit)
        {
            return Physics.Raycast(transform.position,Vector3.down ,out hit , checkerDistance ,
                groundLayer);
        }
        public bool GroundCheck()
        {
            return Physics.Raycast(transform.position,Vector3.down, checkerDistance ,
                groundLayer);
        }

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = checkerColor;
            Gizmos.DrawRay(transform.position,-Vector3.up * checkerDistance);
            Gizmos.color = Color.white;
        }
    }
}