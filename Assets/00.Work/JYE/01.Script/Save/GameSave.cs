using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    public class GameSave : MonoBehaviour
    {
        public GameSaveData data; //저장소
        private string path = "GameSaveData";
        

        private void Awake()
        {
            data = ResetData();
            LoadData();
            
        }

        private void OnDisable()
        {
            SaveData();
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

        private void LoadData() //로드
        {
            string json = PlayerPrefs.GetString(path);
            GameSaveData save = JsonUtility.FromJson<GameSaveData>(json);
            data = save;
        }

        private void SaveData() //저장
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(path, json);
            PlayerPrefs.Save();
        }
    }
}
