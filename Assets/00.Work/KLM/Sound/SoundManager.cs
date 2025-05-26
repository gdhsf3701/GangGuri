using System.Collections.Generic;
using _00.Work.KLM._01.Script.KAY_UI;
using UnityEngine;
using UnityEngine.Audio;

namespace _00.Work.KLM.Sound
{
    [DefaultExecutionOrder(-100)]
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
        public void PlayBGM(SoundName sound)
        {
            if (!clipMap.TryGetValue(sound, out var clip))
            {
                Debug.LogWarning($"[SoundManager] No BGM clip mapped for {sound}");
                return;
            }

            if (!sound.ToString().EndsWith("BGM"))
            {
                Debug.LogWarning($"[SoundManager] {sound} is not a BGM sound.");
                return;
            }

            Debug.Log($"[SoundManager] Playing BGM: {sound}");

            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.outputAudioMixerGroup = bgmGroup;
            bgmSource.Play();
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

        public AudioSource PlayWithLoop(SoundName sound , GameObject target = null)
        {
            if (!clipMap.TryGetValue(sound, out var clip))
            {
                Debug.LogWarning($"[SoundManager] No clip mapped for {sound}");
                return null;
            }

            var newSfxSource = target != null ? target.AddComponent<AudioSource>() : gameObject.AddComponent<AudioSource>();

            newSfxSource.clip = clip;
            newSfxSource.loop = true;
            newSfxSource.outputAudioMixerGroup = sfxGroup;
            newSfxSource.Play();
            
            return newSfxSource;
        }
        
        public void StopPlay(AudioSource source)
        {
            if (source != null)
            {
                source.Stop();
                Destroy(source);
            }
            else
            {
                Debug.LogWarning("[SoundManager] Attempted to stop a null AudioSource.");
            }
        }

        public void SetBGMVolume(float value)
        {
            float dB = Mathf.Approximately(value, 0f) ? -80f : Mathf.Log10(value) * 20f;
            audioMixer.SetFloat("BGMVolume", dB);
        }

        public void SetSFXVolume(float value)
        {
            float dB = Mathf.Approximately(value, 0f) ? -80f : Mathf.Log10(value) * 20f;
            audioMixer.SetFloat("SFXVolume", dB);
        }
    }
}
