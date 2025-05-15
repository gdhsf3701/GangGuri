using UnityEngine;


namespace _00.Work.JYE._01.Script.Save
{
    //저장되는 실질적인 친구.
    [System.Serializable]
    public class GameSaveData
    {
        public int coin; //소유 돈
        public int stage; //스테이지 (개수(1 ~))
        public bool[] playerCar; //소유 차들
        public int car; //차 번째 (0~)
        public string finalDate; //마지막 사용 날짜
    }
}
