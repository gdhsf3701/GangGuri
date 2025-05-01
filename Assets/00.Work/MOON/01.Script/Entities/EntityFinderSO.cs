using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    [CreateAssetMenu(fileName = "EntityFinder", menuName = "SO/EntityFinder", order = 0)]
    public class EntityFinderSO : ScriptableObject
    {
        public Entity Target{get; private set;}

        public void SetTarget(Entity entity)
        {
            Target = entity;
        }
    }
}
