using System;
using _00.Work.KLM.Sound;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.JYE._01.Script.Manager
{
    public class SceneChange : MonoBehaviour
    {
		public static int beforeScene; //이전 씬

        private int curStage; //현재 스테이지

        private void Awake()
        {
            curStage = PlayerPrefs.GetInt("CurStage");
        }

        public void BeforeSceneBtn() //이전 씬으로
        {
            SoundManager.Instance.Play(SoundName.Button);
            SceneBtn(beforeScene);
            SoundManager.Instance.Play(SoundName.Button);
        } 
        public void StageSceneBtn(int i) //스테이지
        {
            
            SoundManager.Instance.Play(SoundName.Button);
            string scene = $"Stage{i}";
            SceneBtn(scene);
        } 
        public void StealSceneBtn() //훔치기
        {
            SoundManager.Instance.Play(SoundName.Button);
            string scene = $"Steal{curStage}";
            SceneBtn(scene);
        } 
        public void FailSceneBtn() //실패
        {
            SoundManager.Instance.Play(SoundName.Button);
            string scene = $"Fail{curStage}";
            if (SceneManager.GetActiveScene().name.Contains("Steal"))
            {
                SceneManager.LoadScene(scene); 
            }
            else
            {
                SceneBtn(scene);
            }
        } 
        public void SuccessSceneBtn() //성공
        { SoundManager.Instance.Play(SoundName.Button);
            string scene = $"Success{curStage}";
            SceneBtn(scene);
        } 

       public void SceneBtn(int i) //해당 씬으로
        { 
            SoundManager.Instance.Play(SoundName.Button);
            Cursor.lockState = CursorLockMode.None;
            beforeScene = SceneManager.GetActiveScene().buildIndex; //현재 씬은 저장
            SceneManager.LoadScene(i);
        }
        public void SceneBtn(string name) //해당 씬으로
        { SoundManager.Instance.Play(SoundName.Button);
            Cursor.lockState = CursorLockMode.None;
            beforeScene = SceneManager.GetActiveScene().buildIndex; //현재 씬은 저장
            SceneManager.LoadScene(name); 
        }

        public void QuitBtn() //끝내기
        { SoundManager.Instance.Play(SoundName.Button);
            Application.Quit();
        }
    }
}
