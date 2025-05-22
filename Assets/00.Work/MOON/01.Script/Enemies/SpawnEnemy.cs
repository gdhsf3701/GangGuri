using _00.Work.MOON._01.Script.Core.DI;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Enemies
{
    public class SpawnEnemy : MonoBehaviour , IDependencyProvider
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private Transform[] spawnPoint;
        [SerializeField] private int spawnCount = 5;
        [SerializeField] private float randomRange = 1f;
        
        [Provide]
        public SpawnEnemy ProvideSpawnEnemy() => this;
        
        public void Spawn()
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Transform enemy = Instantiate(enemyPrefab, GetSpawnTrance() , Quaternion.identity).transform;
                enemy.SetParent(transform);
            }
        }

        private Vector3 GetSpawnTrance()
        {
            int randomIndex = Random.Range(0, spawnPoint.Length);
            Vector3 spawnPosition = spawnPoint[randomIndex].position;
            float rand = randomRange / 2;
            spawnPosition.x += Random.Range(-rand, rand);
            spawnPosition.z += Random.Range(-rand, rand);
            return spawnPosition;
        }
        
        private void OnDrawGizmos()
        {
            if (spawnPoint.Length == 0) return;
            Gizmos.color = Color.red;
            foreach (Transform point in spawnPoint)
            {
                Gizmos.DrawWireSphere(point.position, randomRange);
            }
        }
    }
}
