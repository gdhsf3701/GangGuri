using System;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    //������ �ʿ��� ������ �ʿ� (Ÿ��Ʋ, �ε��... X)
    public class GameSave : MonoBehaviour
    {
        private GameSaveData data; //�����
        private string path;
        
        // saveNum = �����(��) ��ȣ�� �����ϴ�.
        
        private void Start()
        {
            path = SaveManager.Path;
            // data = ResetData();
            data = SaveManager.CurrentData; //�� �ޱ�
        }

        private void OnDisable() //�ڵ� ����
        {
            SaveData(data, path);
        }
        
        public static void SaveData(GameSaveData data, string Path) //�����ϱ�
        {
            data.finalDate = DateTime.Now.ToString("yy�� MM�� dd�� tt HH�� mm��"); //������ �ð� �Է�
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(Path, json);
            PlayerPrefs.Save();
        }

        private GameSaveData ResetData() //������ ����
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
