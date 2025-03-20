using UnityEngine;

namespace _00.Work.MOON._01.Script.Player
{
    public class DropDamage : MonoBehaviour, IEntityComponent
    {
        float maxPosition = 0;

        Entity _entity;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        private void Update()
        {
        
        }
    }
}
