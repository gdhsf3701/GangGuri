using UnityEngine;


namespace _00.Work.JYE._01.Script.Save
{
    [System.Serializable]
    public class GameSaveData
    {
        public int coin; //���� ��
        public int stage; //�������� (����(1 ~))
        public bool[] playerCar; //���� ����
        public int car; //�� ��° (0~)
    }
}
