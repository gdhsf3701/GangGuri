using _00.Work.KLM.Sound;
using _00.Work.MOON._01.Script.SO.Finder;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.CCTV
{
    public class CCTV : MonoBehaviour
    {
        private static bool isEnemy; //true : ¿ÃπÃ µÈ≈¥ / false : æ» µÈ≈¥
        
        [SerializeField]private EnemyManagerFinderSO finder;

        private void Awake()
        {
            isEnemy = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!isEnemy&&other.gameObject.CompareTag("Player"))
            {
                SoundManager.Instance.Play(SoundName.Police);
                isEnemy = true;
                finder.Target.Finded();
            }
    
        }
    }
}
