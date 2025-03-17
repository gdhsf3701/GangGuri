using UnityEngine;

public class GroundChecker : MonoBehaviour, IEntityComponent
{
    [SerializeField] Vector3 checkerSize;
    [SerializeField] Vector3 checkerTransSet;
    [SerializeField] LayerMask groundLayer;
    Entity _entity;

    public bool GroundCheck()
    {
        return Physics.CheckBox(transform.position + checkerTransSet, checkerSize, Quaternion.identity, groundLayer);
    }

    public void Initialize(Entity entity)
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
