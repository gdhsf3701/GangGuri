using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public class TurnOverRotate : MonoBehaviour,IEntityComponent
    {
        [SerializeField] Vector3 checkerSize;
        [SerializeField] LayerMask groundLayer;
        #region testEntityGizmos
        [SerializeField]private Entity _entity;
        [SerializeField] private Color checkerColor;
        [SerializeField] private Mesh box;
        #endregion
        
        [SerializeField] private float time;
        private float timer;
        
        public bool TurnOverCheck()
        {
            return Physics.CheckBox(transform.position, checkerSize, _entity.transform.rotation,
                groundLayer);
        }

        private void Update()
        {
            if (TurnOverCheck())
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    _entity.transform.rotation = new Quaternion(0, transform.rotation.y , 0 , transform.rotation.w);
                    timer = time; 
                }
            }
            else
            {
                timer = time;
            }
        }

        public void Initialize(Entity entity)
        {
            _entity = entity;
            timer = time;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = checkerColor;
            Gizmos.DrawWireMesh(box, transform.position, _entity.transform.rotation, checkerSize);
            Gizmos.color = Color.white;
        }
    }
}
