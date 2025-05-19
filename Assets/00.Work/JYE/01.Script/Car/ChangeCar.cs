using _00.Work.JYE._01.Script.Save;
using _00.Work.MOON._01.Script.Players;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Car
{
    public class ChangeCar : MonoBehaviour
    {
        
        private PlayerMovement move;
        
        private GameSaveData curData; //현재 저장된 데이터
        
        private void Awake()
        {
            curData = SaveManager.CurrentData;
            
            
            move.ChangeStat(curData.car == ""? "Model1" : curData.car);
        }
    }
}
