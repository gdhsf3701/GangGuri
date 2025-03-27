using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _00.Work.KLM._01.Script.KAY_UI
{
    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer myMixer;
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider SFXSlider;

        private void Start()
        {
            if (PlayerPrefs.HasKey("BGMVolume"))
            {
                LoadVolume();
                //아니 이 미친 것
            }
            else
            {
                SetMusicVolum();
                SetSFXVolum();
            }
        
        }
        public void SetMusicVolum()
        {
            float volum = musicSlider.value;
            myMixer.SetFloat("BGMVolume", Mathf.Log10(volum)*20);
            PlayerPrefs.SetFloat("BGMVolume",volum);
        }
        public void SetSFXVolum()
        {
            float volum = SFXSlider.value;
            myMixer.SetFloat("SFXVolume", Mathf.Log10(volum) * 20);
            PlayerPrefs.SetFloat("SFXVolume", volum);
        }
        private void LoadVolume()
        {
            musicSlider.value = PlayerPrefs.GetFloat("BGMVolume");
            SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");

            SetMusicVolum();
        }
    }
}