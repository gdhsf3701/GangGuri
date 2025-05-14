using System;
using System.Collections;
using _00.Work.JYE._01.Script.Manager;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Save
{
    //메인 씬에만 필요
    public class MainSaveCheck : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private SceneChange sceneManager; //씬 바꾸기
        [SerializeField] private GameObject warning; //강제 이동 경고
        
        private bool isEmpty;//true : 괜찮음, false : 비워주러 강제 로드씬
        private void Start()
        {      
            warning.SetActive(false);
            isEmpty = System.Convert.ToBoolean(PlayerPrefs.GetInt("SaveFileEmpty")); //bool 값으로 전환
            if (!isEmpty) //꽉 찼는지
            {
                StartCoroutine(ForciblyLoadScene());
            }
            else
            {
                SaveManager.Instance.TitleCheckChange(3); //인게임 시작이라고
            }
        }

        private IEnumerator ForciblyLoadScene() //강제로 로드씬으로
        {
            warning.SetActive(true);
            
            yield return new WaitForSeconds(3f);
            sceneManager.SceneBtn(2);
        }
    }
}
