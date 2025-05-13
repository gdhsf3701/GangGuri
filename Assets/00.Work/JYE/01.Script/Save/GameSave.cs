using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    //������ �ʿ��� ������ �ʿ� (Ÿ��Ʋ, �ε��... X)
    public class GameSave : MonoBehaviour
    {
        private GameSaveData data; //�����
        
        // saveNum = �����(��) ��ȣ�� �����ϴ�.
        
        private void Start()
        {
            // data = ResetData();
            data = SaveManager.CurrentData; //�� �ޱ�
        }

        private void OnDisable() //�ڵ� ����
        {
            SaveManager.Instance.SaveData(data);
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
    }
}
