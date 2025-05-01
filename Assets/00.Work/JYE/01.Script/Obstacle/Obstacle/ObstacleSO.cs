using UnityEngine;

namespace _00.Work.JYE._01.Script.Obstacle.Obstacle
{
    [CreateAssetMenu(fileName = "obstacleSO", menuName = "SO/obstacleSO")]
    public class ObstacleSO : ScriptableObject //��ֹ�
    {
        public eventType myEvent;
        public obstacleType myObstacle;


    }

    public enum eventType
    {
        none,
        hailstone, //���
        fog, //�Ȱ�
    }

    public enum obstacleType
    {
        none, //����
        cctv, //cctv
        bomb, //��ź
        ice, //���� ��
        bumpy, //���������� ��
        sinkhole,//��ũȦ
        manhole, //��Ȧ
        car, //�ڵ���
        trash, //������
        telephonePole, //������

    }
}
