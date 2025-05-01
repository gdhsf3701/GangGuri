using _00.Work.MOON._01.Script.Core.DI;
using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.Players;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Managers
{
    [DefaultExecutionOrder(-1)]
    public class PlayerManager : MonoBehaviour
    {
        [Inject] private Player player;
        [SerializeField] private EntityFinderSO playerFinder;
        
        private void Awake()
        {
            playerFinder.SetTarget(player);
        }
    }
}