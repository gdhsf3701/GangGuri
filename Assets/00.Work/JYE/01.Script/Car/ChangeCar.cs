using _00.Work.JYE._01.Script.Save;
using _00.Work.MOON._01.Script.Players;
using _00.Work.MOON._01.Script.SO.Finder;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Car
{
    public class ChangeCar : MonoBehaviour
    {
        
        [SerializeField]private PlayerFinderSO playerFinder;
        
        private GameSaveData curData; //현재 저장된 데이터
        
        private void Start()
        {
            curData = SaveManager.CurrentData;
            
            
            playerFinder.Target.GetCompo<PlayerMovement>().ChangeStat(curData.car == ""? "Model1" : curData.car);
        }
    }
}
