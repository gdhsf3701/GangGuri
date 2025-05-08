using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.SO.Entity;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace _00.Work.MOON._01.Script.Enemies
{
    public class EnemyMovement : EntityMovement
    {
        [SerializeField] NavMeshAgent agent;
        bool isStop = false;

        public override void ChangeStat(string statType)
        {
            base.ChangeStat(statType); 
            agent.speed = _moveSpeed;
            agent.angularSpeed = _rotateSpeed * Mathf.Rad2Deg;
            agent.stoppingDistance = _stopDistance;
        }

        public void SetDestination(Vector3 destination) => agent.SetDestination(destination);

        public void SetStop(bool isStop) => agent.isStopped = isStop;
    }
}
