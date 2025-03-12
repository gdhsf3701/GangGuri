using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] Vector3 checkerSize;
    [SerializeField] Vector3 checkerTransSet;

    public void GroundCheck()
    {
        Physics.OverlapBox(transform.position + checkerTransSet, checkerSize);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + checkerTransSet, checkerSize);
        Gizmos.color = Color.white;
    }
}
