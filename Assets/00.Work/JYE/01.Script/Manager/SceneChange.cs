using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.JYE._01.Script.Manager
{
    public class SceneChange : MonoBehaviour
    {
		public static string beforeScene; //이전 씬

        public void BeforeSceneBtn() //이전 씬으로
        {
            SceneBtn(beforeScene);
        } 
        public void StageSceneBtn(int i) //스테이지
        {
            string scene = $"Stage{i}";
            SceneBtn(scene);
        } 
        public void FailSceneBtn(int i) //실패
        {
            string scene = $"Fail{i}";
            SceneBtn(scene);
        } 
        public void SuccessSceneBtn(int i) //성공
        {
            string scene = $"Success{i}";
            SceneBtn(scene);
        } 

       public void SceneBtn(int i) //해당 씬으로
        {
            beforeScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(i);
        }
        public void SceneBtn(string name) //해당 씬으로
        {
            beforeScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(name); 
        }

        public void QuitBtn() //끝내기
        {
            Application.Quit();
        }
    }
}
