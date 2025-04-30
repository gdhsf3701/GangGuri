using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    public class GameSave : MonoBehaviour
    {
        public GameSaveData data; //�����
        private string path = "GameSaveData";

        private void Awake()
        {
            data = new GameSaveData();
            SaveData();
        }

        private void SaveData() //����
        {
            string json = PlayerPrefs.GetString(path);
            GameSaveData save = JsonUtility.FromJson<GameSaveData>(json);
            data = save;
        }

        private void LoadData() //�ε�
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(path, json);
            PlayerPrefs.Save();
        }
    }
}
