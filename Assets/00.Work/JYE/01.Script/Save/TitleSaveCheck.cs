using System.IO;
using UnityEngine;


namespace _00.Work.JYE._01.Script.Save
{
    //무조건 타이틀에만 존재 해야 함
    public class TitleSaveCheck : MonoBehaviour
    {
        private string path;
        private int currentNum; // 현재 번호 (파일 만들 번호)
        private bool isEmpty; //true : 비었음, false : 꽉 찼음(비워줘야 함)


        private void Start()
        {   
            path =  SaveManager.Path;

            if (SaveManager.TitleCheck == 3) //역시 인게임중 첫 타이틀이라면 (즉 첫 로드)
            {
                SaveManager.Instance.TitleCheckChange(-1);
            }

            currentNum = SaveManager.SaveCurrentNum;
            //print($"{NumCheck()} && {EmptyCheck()} && {SaveManager.TitleCheck} <= 0");
            if (NumCheck() && EmptyCheck() && SaveManager.TitleCheck <= 0) //체크 && 타이틀 못 지났다면
            {
                SaveManager.Instance.TitleCheckChange(1); //넘겼다, 야호!
                isEmpty = true;
                SaveFileMake(); //파일 만들어주기
            }
            else if (SaveManager.TitleCheck >= 1) //1은 이미 넘긴건데 뭐가 중요해
            {
                isEmpty = true;
            }
            else
            {
                SaveManager.Instance.TitleCheckChange(0);
                isEmpty = false;
            }

            PlayerPrefs.SetInt("SaveFileEmpty", System.Convert.ToInt16(isEmpty)); //bool 값을 넣어주기
            //이 값에 따라
            //메인으로 가면 노빠꾸 선택씬으로,
            //선택씬 오면 하나 삭제하라고 강요.
        }
        private void SaveFileMake() //새 파일 만들기
        {
            string data = PlayerPrefs.GetString(path);
            
            if(currentNum <= 0)
                currentNum = SaveManager.SaveCurrentNum;

            if (!Directory.Exists($"{path}"))
            {
                Directory.CreateDirectory($"{path}");
            }

            ReSaveFile();
            File.WriteAllText($"{path}/{currentNum}", data); //새 파일 만들기   

            //File.AppendAllText($"{path}/allNum", $"{currentNum}\n"); // 전체 저장에 숫자 집어넣기
        }

        private void ReSaveFile() //같은 파일있음 삭제
        {
            if (Directory.Exists($"{path}/{currentNum}"))
            {
                File.Delete($"{path}/{currentNum}");
            }
        }

        private bool NumCheck() //파일 개수 초과인지 체크
        {
            if (SaveManager.Instance.MaxNum <= currentNum) //수를 초과
            {
                return false;
            }

            return true;
        }

        private bool EmptyCheck() // 저장된 데이터가 빈 건지 체크
        {
            GameSaveData emptyData = new GameSaveData(); //빈 거
            string empty = JsonUtility.ToJson(emptyData);

            if (empty == PlayerPrefs.GetString(path)) //비었음.
            {
                return false;
            }

            return true;
        }
    }
}
