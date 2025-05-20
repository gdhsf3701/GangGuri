using UnityEngine;


namespace _00.Work.JYE._01.Script.Save
{
    //저장되는 실질적인 친구.
    [System.Serializable]
    public class GameSaveData
    {
        public int stage; //스테이지 (개수(1 ~))
        public bool[] playerCar; //소유 차들
        public string car; //사용 중이던 차 이름 (Model 1~)
        public string finalDate; //마지막 사용 날짜
    }
}
