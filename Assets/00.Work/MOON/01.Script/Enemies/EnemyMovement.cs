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

        public override void ChangeStat(string statType)
        {
            base.ChangeStat(statType);
            
        }

        public void SetDestination(Vector3 valuePosition)
        {
            throw new System.NotImplementedException();
        }

        public void SetStop(bool newValueValue)
        {
            throw new System.NotImplementedException();
        }
    }
}
