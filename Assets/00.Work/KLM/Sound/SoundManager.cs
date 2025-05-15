using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _00.Work.KLM._01.Script.KAY_UI
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [Header("Mixer")]
        [SerializeField] private AudioMixer audioMixer;

        [Header("BGM Settings")]
        [Tooltip("다수의 BGM 클립을 이 리스트에 추가하세요")]
        [SerializeField] private List<AudioClip> bgmClips = new List<AudioClip>();
        [SerializeField] private AudioSource[] bgmSources = new AudioSource[2];
        [SerializeField] private float bgmFadeTime = 1f;

        [Header("SFX Settings")]
        [Tooltip("다수의 SFX 클립을 이 리스트에 추가하세요")]
        [SerializeField] private List<AudioClip> sfxClips = new List<AudioClip>();
        [SerializeField] private AudioSource sfxPrefab;
        [SerializeField] private int sfxPoolSize = 10;

        [Header("UI Sliders")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider sfxSlider;

        [Header("Ducking Settings")]
        [SerializeField] private float duckingVolumeDB = -15f;
        [SerializeField] private float duckTransitionTime = 0.3f;

        private const string BGM_PARAM = "BGMVolume";
        private const string SFX_PARAM = "SFXVolume";

        private Queue<AudioSource> sfxPool = new Queue<AudioSource>();
        private float defaultBgmDB;
        private int activeBgmSource = 0;

        private void Awake()
        {
            // 싱글톤 설정
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // SFX 풀 초기화
            for (int i = 0; i < sfxPoolSize; i++)
            {
                var src = Instantiate(sfxPrefab, transform);
                src.playOnAwake = false;
                sfxPool.Enqueue(src);
            }

            // BGM 소스 초기화
            foreach (var src in bgmSources)
            {
                src.playOnAwake = false;
                src.loop = true;
            }

            // 기본 BGM 볼륨 보관
            audioMixer.GetFloat(BGM_PARAM, out defaultBgmDB);

            // 볼륨 불러오기 & 슬라이더 세팅
            float mus = PlayerPrefs.GetFloat(BGM_PARAM, 1f);
            float sfx = PlayerPrefs.GetFloat(SFX_PARAM, 1f);
            musicSlider.value = mus;
            sfxSlider.value = sfx;
            ApplyVolume(BGM_PARAM, mus);
            ApplyVolume(SFX_PARAM, sfx);

            // UI 콜백
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
            sfxSlider.onValueChanged.AddListener(SetSfxVolume);
        }

        #region Volume

        public void SetMusicVolume(float sliderVal)
        {
            PlayerPrefs.SetFloat(BGM_PARAM, sliderVal);
            ApplyVolume(BGM_PARAM, sliderVal);
        }

        public void SetSfxVolume(float sliderVal)
        {
            PlayerPrefs.SetFloat(SFX_PARAM, sliderVal);
            ApplyVolume(SFX_PARAM, sliderVal);
        }

        private void ApplyVolume(string param, float sliderVal)
        {
            float db = Mathf.Log10(Mathf.Max(sliderVal, 0.0001f)) * 20f;
            audioMixer.SetFloat(param, db);
        }

        #endregion

        #region BGM Playlist & Dual-Source Crossfade

        /// <summary>
        /// 리스트 인덱스로 BGM 재생
        /// </summary>
        public void PlayBgm(int clipIndex)
        {
            if (clipIndex < 0 || clipIndex >= bgmClips.Count) return;
            PlayBgm(bgmClips[clipIndex]);
        }

        /// <summary>
        /// AudioClip 직접 재생
        /// </summary>
        public void PlayBgm(AudioClip clip)
        {
            StopAllCoroutines();
            StartCoroutine(CrossfadeBgm(clip));
        }

        private IEnumerator CrossfadeBgm(AudioClip newClip)
        {
            int next = 1 - activeBgmSource;
            var currentSrc = bgmSources[activeBgmSource];
            var nextSrc    = bgmSources[next];

            // 준비: next 소스에 클립 설정 후 볼륨 0으로 플레이
            nextSrc.clip = newClip;
            nextSrc.volume = 0f;
            nextSrc.Play();

            // 페이드
            float elapsed = 0f;
            while (elapsed < bgmFadeTime)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = elapsed / bgmFadeTime;
                currentSrc.volume = Mathf.Lerp(1f, 0f, t);
                nextSrc.volume    = Mathf.Lerp(0f, 1f, t);
                yield return null;
            }

            // 전환 완료
            currentSrc.Stop();
            currentSrc.volume = 1f;
            nextSrc.volume    = 1f;
            activeBgmSource = next;
        }

        public void StopBgmImmediate()
        {
            foreach (var src in bgmSources) src.Stop();
        }

        #endregion

        #region SFX Pooling & Play by Name/Index

        /// <summary>
        /// 인덱스 기반 SFX 재생
        /// </summary>
        public void PlaySfx(int clipIndex)
        {
            if (clipIndex < 0 || clipIndex >= sfxClips.Count) return;
            PlaySfx(sfxClips[clipIndex]);
        }

        /// <summary>
        /// 이름 기반 SFX 재생 (리스트에서 처음 찾은 이름)
        /// </summary>
        public void PlaySfx(string clipName)
        {
            var clip = sfxClips.Find(c => c != null && c.name == clipName);
            if (clip != null) PlaySfx(clip);
        }

        /// <summary>
        /// AudioClip 직접 SFX 재생
        /// </summary>
        public void PlaySfx(AudioClip clip)
        {
            if (sfxPool.Count == 0 || clip == null) return;
            var src = sfxPool.Dequeue();
            src.clip = clip;
            src.Play();
            StartCoroutine(ReturnToPool(src));
        }

        private IEnumerator ReturnToPool(AudioSource src)
        {
            yield return new WaitWhile(() => src.isPlaying);
            sfxPool.Enqueue(src);
        }

        #endregion

        #region Ducking Without Snapshots

        public void StartDucking()
        {
            StopCoroutine("DuckRoutine");
            StartCoroutine(DuckRoutine(defaultBgmDB, duckingVolumeDB, duckTransitionTime));
        }

        public void StopDucking()
        {
            StopCoroutine("DuckRoutine");
            StartCoroutine(DuckRoutine(duckingVolumeDB, defaultBgmDB, duckTransitionTime));
        }

        private IEnumerator DuckRoutine(float fromDB, float toDB, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                float cur = Mathf.Lerp(fromDB, toDB, elapsed / duration);
                audioMixer.SetFloat(BGM_PARAM, cur);
                yield return null;
            }
            audioMixer.SetFloat(BGM_PARAM, toDB);
        }

        #endregion
    }
}
