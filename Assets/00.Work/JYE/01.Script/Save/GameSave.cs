using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    public class GameSave : MonoBehaviour
    {
        private GameSaveData data; //�����
        private string path;
        private static int saveNum; //�����(��) ��ȣ
        
        // saveNum = �����(��) ��ȣ�� �����ϴ�.
        private void Awake()
        {
            path =  Application.persistentDataPath+"GameSaveData";
            saveNum = PlayerPrefs.GetInt("saveNum");
            
            // data = ResetData();
            // LoadData();
        }

        private void OnDisable() //�ڵ� ����
        {
            SaveData();
        }

        public int GetSaveNum()
        {
            return saveNum;
        }

        public string GetPath() //����� �˷��ֱ�.
        {
            return path;
        }

        private GameSaveData ResetData() //������ ����
        {
            GameSaveData d =  new GameSaveData();
            d.coin = 0;
            d.playerCar = new bool[5];
            d.stage = 1;
            d.car = 0;
            return d;
        }

        private void LoadData() //�ε�
        {
            string json = PlayerPrefs.GetString(path);
            GameSaveData save = JsonUtility.FromJson<GameSaveData>(json);
            data = save;
        }

        private void SaveData() //����
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(path, json);
            PlayerPrefs.Save();
        }
    }
}
