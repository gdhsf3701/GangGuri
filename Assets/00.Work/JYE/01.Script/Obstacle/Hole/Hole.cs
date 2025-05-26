using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.Hole
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;
        [SerializeField] private Transform pos; //리스폰 될 위치

        private void OnTriggerEnter(Collider other)
        {
            particle.Play();
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.position = pos.position; //위치 이동
            }
        }

    }
}
