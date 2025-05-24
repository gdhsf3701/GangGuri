using System;
using _00.Work.KLM._01.Script.KAY_UI;
using _00.Work.KLM.Sound;
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
        SoundManager.Instance.Play(SoundName.Button);
        realQuitPanel.SetActive(true);
    }
    
    
    public void Settings()
    {
        SoundManager.Instance.Play(SoundName.Button);
        settingPanel.SetActive(!settingPanel.activeSelf);
        
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.Instance.Play(SoundName.Button);
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
        SoundManager.Instance.Play(SoundName.Button);
        realQuitPanel.SetActive(false);
    }

    public void Quit()
    {
        SoundManager.Instance.Play(SoundName.Button);
        Application.Quit();
    }
    
}
