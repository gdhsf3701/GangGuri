using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    //저장이 필요한 모든씬에 필요 (타이틀, 로드씬... X)
    public class GameSave : MonoBehaviour
    {
        private GameSaveData data; //저장소
        
        // saveNum = 저장된(할) 번호를 저장하는.
        
        private void Start()
        {
            // data = ResetData();
            data = SaveManager.CurrentData; //값 받기
        }

        private void OnDisable() //자동 저장
        {
            SaveManager.Instance.SaveData(data);
        }

        private GameSaveData ResetData() //데이터 리셋
        {
            GameSaveData d =  new GameSaveData();
            d.coin = 0;
            d.playerCar = new bool[5];
            d.stage = 1;
            d.car = 0;
            return d;
        }
    }
}
