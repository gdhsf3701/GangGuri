using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject realQuitPanel;
    [SerializeField] private GameObject settingPanel;

    private void Awake()
    {
        realQuitPanel.SetActive(false);
        settingPanel.SetActive(false);
    }

    public void RealQuit()
    {
        realQuitPanel.SetActive(true);
    }
    
    public void Settings()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
        
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            if(realQuitPanel.activeSelf == false)
                settingPanel.SetActive(!settingPanel.activeSelf);
            else
            {
                realQuitPanel.SetActive(false);
            }
        }

        if (settingPanel.activeSelf == false)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void RealClose()
    {
        realQuitPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
