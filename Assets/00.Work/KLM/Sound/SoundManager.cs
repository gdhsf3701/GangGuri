using System.Collections.Generic;
using _00.Work.KLM._01.Script.KAY_UI;
using UnityEngine;
using UnityEngine.Audio;

namespace _00.Work.KLM.Sound
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioMixerGroup bgmGroup;
        [SerializeField] private AudioMixerGroup sfxGroup;

        private Dictionary<SoundName, AudioClip> clipMap = new Dictionary<SoundName, AudioClip>();

        private void Awake()
        {
            if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
            else { Destroy(gameObject); }

            LoadAllClips();
        }

        private void LoadAllClips()
        {
            foreach (SoundName sound in System.Enum.GetValues(typeof(SoundName)))
            {
                string path = sound.ToString().StartsWith("Stage") || sound.ToString().EndsWith("BGM") || sound == SoundName.TitleBGM || sound == SoundName.HomeBGM || sound == SoundName.StageSelectBGM
                    ? $"Audio/BGM/{sound}"
                    : $"Audio/SFX/{sound}";

                AudioClip clip = Resources.Load<AudioClip>(path);
                if (clip != null)
                    clipMap[sound] = clip;
                else
                    Debug.LogWarning($"[SoundManager] Clip not found at: Resources/{path}");
            }
        }

        public void Play(SoundName sound)
        {
            if (!clipMap.TryGetValue(sound, out var clip))
            {
                Debug.LogWarning($"[SoundManager] No clip mapped for {sound}");
                return;
            }

            if (sound.ToString().EndsWith("BGM"))
            {
                bgmSource.clip = clip;
                bgmSource.loop = true;
                bgmSource.outputAudioMixerGroup = bgmGroup;
                bgmSource.Play();
            }
            else
            {
                sfxSource.outputAudioMixerGroup = sfxGroup;
                sfxSource.PlayOneShot(clip);
            }
        }

        public void SetBGMVolume(float value) => audioMixer.SetFloat("BGMVolume", Mathf.Lerp(-80, 0, value));
        public void SetSFXVolume(float value) => audioMixer.SetFloat("SFXVolume", Mathf.Lerp(-80, 0, value));
    }
}
