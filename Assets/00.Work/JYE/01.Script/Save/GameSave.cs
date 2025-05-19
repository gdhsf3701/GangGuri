using System;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    //저장이 필요한 모든씬에 필요 (타이틀, 로드씬... X)
    public class GameSave : MonoBehaviour
    {
        private GameSaveData data; //저장소
        private string path;
        
        // saveNum = 저장된(할) 번호를 저장하는.
        
        private void Start()
        {
            path = SaveManager.Path;
            // data = ResetData();
            data = SaveManager.CurrentData; //값 받기
        }

        private void OnDisable() //자동 저장
        {
            SaveData(data, path);
        }
        
        public static void SaveData(GameSaveData data, string Path) //저장하기
        {
            data.finalDate = DateTime.Now.ToString("yy년 MM월 dd일 tt HH시 mm분"); //마지막 시간 입력
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(Path, json);
            PlayerPrefs.Save();
        }

        private GameSaveData ResetData() //데이터 리셋
        {
            GameSaveData d =  new GameSaveData();
            d.coin = 0;
            d.playerCar = new bool[5];
            d.stage = 1;
            d.car = "Model1";
            return d;
        }
    }
}
