using UnityEngine;


namespace _00.Work.JYE._01.Script.Save
{
    //����Ǵ� �������� ģ��.
    [System.Serializable]
    public class GameSaveData
    {
        public int stage; //�������� (����(1 ~))
        public bool[] playerCar; //���� ����
        public string car; //��� ���̴� �� �̸� (Model 1~)
        public string finalDate; //������ ��� ��¥
    }
}
