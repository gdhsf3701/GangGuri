using _00.Work.MOON._01.Script.Players;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Respawn
{
    public class Respawn : MonoBehaviour
    {
        private PlayerMovement move;
        [SerializeField] private Transform map;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Teleport(other.transform);
            }
        }

        private void Teleport(Transform obj)
        {
            Rigidbody rigid = obj.GetComponent<Rigidbody>();
            rigid.constraints = RigidbodyConstraints.FreezeAll; //제한
            
            float mapZRo = Mathf.Abs(map.rotation.z); //맵의 기울기
            
            Vector3 finalPos = new Vector3(obj.position.x,map.position.y + 5,map.position.z); //좌표
            print($"({obj.position.x} - {map.position.y}) * ({mapZRo}) = {finalPos.y} ");

            // 이동
            obj.position = finalPos;
            move = obj.gameObject.GetComponentInChildren<PlayerMovement>();
            rigid.constraints = RigidbodyConstraints.FreezeRotation; //제한 풀기
        }
    }
}