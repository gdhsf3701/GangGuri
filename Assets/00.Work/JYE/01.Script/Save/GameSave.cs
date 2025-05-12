using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    public class GameSave : MonoBehaviour
    {
        private GameSaveData data; //저장소
        private string path;
        private static int saveNum; //저장된(할) 번호
        
        // saveNum = 저장된(할) 번호를 저장하는.
        private void Awake()
        {
            path =  Application.persistentDataPath+"GameSaveData";
            saveNum = PlayerPrefs.GetInt("saveNum");
            
            // data = ResetData();
            // LoadData();
        }

        private void OnDisable() //자동 저장
        {
            SaveData();
        }

        public int GetSaveNum()
        {
            return saveNum;
        }

        public string GetPath() //저장소 알려주기.
        {
            return path;
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
