using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject realQuitPanel;
    [SerializeField] private GameObject settingPanel;


    public void RealQuit()
    {
        realQuitPanel.SetActive(true);
    }

    public void NextScene()
    {
        //씬매니저 넣어야죠
    }

    public void SaveScene()
    {
      //씬 매니저 넣어야죠    
    }

    public void Settings()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
