using System;
using System.IO;
using _00.Work.JYE._01.Script.Save;
using UnityEngine;

public class SaveCheck : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private int Num = 5;  //최대 저장 가능 번호
    
    [SerializeField] private GameSave  gameSave; //저장 및 로드 해주는 스크립트.
    
    private string path;
    private bool isEmpty; //true : 비었음, false : 꽉 찼음(비워줘야 함)
    
    private void Awake()
    {
        path = gameSave.GetPath();

        if (NumCheck() && EmptyCheck())
        {
            isEmpty = true;
            SaveFileMake();
        }
        else
        {
            isEmpty = false;
            //메인으로 가면 노빠꾸 선택씬으로,
            //선택씬 오면 하나 삭제하라고 강요.
        }
    }

    public bool IsEmpty() //지워줘야 하는지 알려주기 (저장 공간 다 찼는지)
    {
        return isEmpty;
    }

    private void SaveFileMake() //새 파일 만들기
    {
        string data = PlayerPrefs.GetString(path);
        File.WriteAllText($"{path}/{Num}", data); //새 파일 만들기

        File.AppendAllText($"{path}/allNum", $"{Num}\n"); // 전체 저장에 숫자 집어넣기
    }

    private bool NumCheck() //현재 개수 체크
    {
        if (Num < gameSave.GetSaveNum()) //수를 초과
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
