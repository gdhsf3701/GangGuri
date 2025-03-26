using UnityEngine;

namespace _00.Work.MOON._01.Script
{
    public class CamaraRotateChage : MonoBehaviour
    {
        [SerializeField]private Transform _playerTrans;

        private void FixedUpdate()
        {
            Move();
        }
        private void Move()
        {
            Vector3 changedEuler = transform.eulerAngles;
            changedEuler.y = _playerTrans.eulerAngles.y + 90;
            transform.eulerAngles = changedEuler;
        }
    }
}
