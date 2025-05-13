using System;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    //무언 갈 할 때 얘 부터 확인 할 것.
    //모든 씬에 필요. (saveManager에 둘 것.)
    public class SaveManager : Singleton<SaveManager>
    {
        public static string Path; //저장 할 곳

        public static int AllSaveNum; //현재 저장 되어 있는 슬롯 수
                                      //pp의 saveNum 에 저장 되어 있음
                                      // 1 ~ max
        public static int SaveCurrentNum { get; private set; } //저장할 슬롯 넘버(다음에 저장 되면 이곳에 저장.)
                                                               //(받아온 값 혹은 현재 저장 되어 있는 슬롯 수 +1 값)
                                                               // 1 ~ max

        public static GameSaveData CurrentData; //현재 저장 내용

        public int MaxNum { get; private set; } = 5;//최대 저장 가능 번호

        public int TitleCheck { get; private set; } //타이틀 갔는지 (중복 땜)
        // -1 : 타이틀을 드디어옴(0혹은 2어야지만 바뀌는게 가능) / 0 : 저장 초과 / 1 : 새게임 (현재 넘버 올림 받음) / 2 : 메인씬 감. (저장 초과X) (1때만 가능)
        // pp의 titleCheck 에 저장되어 있음

        private void Awake()
        {
            //SetTest(5);
            
            TitleCheck = PlayerPrefs.GetInt("titleCheck");
            Path = Application.persistentDataPath + "/GameSaveData"; 
            AllSaveNum = PlayerPrefs.GetInt("saveNum");
            SaveCurrentNum = AllSaveNum; // 값 안 받아오면 자동으로 저장되어 있는 슬롯 수 + 1 값이 됨.
            LoadData();
            

            print(Path);
        }

        private void Update()
        {
            print($"cur : {SaveCurrentNum} / all : {AllSaveNum}");
        }

        private void SetTest(int num)//빌드 본 때는 꼭 없애기
        {
            PlayerPrefs.SetInt("saveNum", num);
            PlayerPrefs.Save();   
        }

        public void NewGame() //저장 슬롯 더해주기 (그러니까 새 게임)
        {
            if (TitleCheck >= 1) //0과 -1 은 사실상 초과인 것도 모르니까
            {  
                SetSaveNum(1);
            }
        }

        public void SetSaveNum(int num) //현재 저장 슬롯수 정해주기 (삭제나 추가 할때)
        {
            AllSaveNum+= num;
            PlayerPrefs.SetInt("saveNum", AllSaveNum);
            PlayerPrefs.Save(); 
        }
        public void SetSaveData(GameSaveData data, int num) //받아온 값을 현재 본인 값으로
        {
            SaveCurrentNum = num;
            CurrentData = data;
            SaveData(CurrentData);
            
            TitleCheckChange(1); //일단 타이틀 넘겼다고 (삭제 했든 안 했든 일단 해당 파일에 저장 할거니까)
        }

        public void TitleCheckChange(int num) //타이틀 넘버 수 정해주기 (위 변수 주석 보기)
        {
            TitleCheck = num;
            PlayerPrefs.SetInt("titleCheck", TitleCheck);
            PlayerPrefs.Save();
        }
        
        public void SaveData(GameSaveData data) //저장하기
        {
            data.finalDate = DateTime.Now.ToString("yy년 MM월 dd일 tt HH시 mm분"); //마지막 시간 입력
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(Path, json);
            PlayerPrefs.Save();
        }
        private void LoadData() //데이터 로드
        {
            string data = PlayerPrefs.GetString(Path);
            CurrentData = JsonUtility.FromJson<GameSaveData>(data);
        }
    }
}
