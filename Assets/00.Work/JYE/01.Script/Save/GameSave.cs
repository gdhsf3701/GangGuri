using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    public class GameSave : MonoBehaviour
    {
        public GameSaveData data; //저장소
        private string path = "GameSaveData";

        private void Awake()
        {
            data = new GameSaveData();
            SaveData();
        }

        private void SaveData() //저장
        {
            string json = PlayerPrefs.GetString(path);
            GameSaveData save = JsonUtility.FromJson<GameSaveData>(json);
            data = save;
        }

        private void LoadData() //로드
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(path, json);
            PlayerPrefs.Save();
        }
    }
}
