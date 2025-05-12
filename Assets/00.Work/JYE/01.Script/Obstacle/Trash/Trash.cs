using System;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.Trash
{
    public class Trash : MonoBehaviour
    {
        [SerializeField] private GameObject slowFloor; // 바닥

        private void OnCollisionEnter(Collision other)
        {           
            if (other.gameObject.CompareTag("Player"))
            {
                Vector3 pos = transform.position - (transform.localScale * 0.3f);
                
                GameObject floor = Instantiate(slowFloor,pos , transform.rotation);
                floor.SetActive(true);
                
                Destroy(gameObject);
            }
        }
    }
}
