using _00.Work.MOON._01.Script.Enemies;
using _00.Work.MOON._01.Script.SO.Finder;
using Unity.Behavior;
using UnityEngine;
using Action = System.Action;

namespace _00.Work.JYE._01.Script.Obstacle.CCTV
{
    public class CCTV : MonoBehaviour
    {
        private EnemyManagerFinderSO finder;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                finder.Target.Finded();
            }
    
        }
    }
}
