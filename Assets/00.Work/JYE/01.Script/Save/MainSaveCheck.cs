using System;
using _00.Work.JYE._01.Script.Manager;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    //메인 씬에만 필요
    public class MainSaveCheck : MonoBehaviour
    {
        [SerializeField] private SceneChange sceneManager; //씬 바꾸기
        private bool isEmpty;//true : 괜찮음, false : 비워주러 강제 로드씬
        private void Start()
        {      
            isEmpty = System.Convert.ToBoolean(PlayerPrefs.GetInt("SaveFileEmpty")); //bool 값으로 전환
            if (!isEmpty) //꽉 찼는지
            {
                ForciblyLoadScene();
            }
            else
            {
                SaveManager.Instance.TitleCheckChange(2); //인게임 시작이라고
            }
        }

        private void ForciblyLoadScene() //강제로 로드씬으로
        {
            sceneManager.SceneBtn(1);
        }
    }
}
