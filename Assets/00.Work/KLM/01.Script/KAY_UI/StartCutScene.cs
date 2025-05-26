using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartCutScene : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> sets;

    [SerializeField] private float fadeDuration = 0.3f;
    [SerializeField] private float displayDuration = 3.0f;
    [SerializeField] private string nextSceneName;

    private void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        foreach (GameObject set in sets)
        {
            // 자식 Image & TextMeshProUGUI 찾기
            Image image = set.GetComponentInChildren<Image>(true);
            TextMeshProUGUI text = set.GetComponentInChildren<TextMeshProUGUI>(true);

            // 시작 시 알파 0으로 설정
            SetAlpha(image, 0f);
            SetAlpha(text, 0f);

            set.SetActive(true);

            // 페이드 인
            yield return StartCoroutine(Fade(image, text, 0f, 1f, fadeDuration));

            // 표시 유지
            yield return new WaitForSeconds(displayDuration);

            // 페이드 아웃
            yield return StartCoroutine(Fade(image, text, 1f, 0f, fadeDuration));

            set.SetActive(false);
        }

        // 다음 씬으로 전환
        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator Fade(Image image, TextMeshProUGUI text, float from, float to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            float t = time / duration;
            float alpha = Mathf.Lerp(from, to, t);
            SetAlpha(image, alpha);
            SetAlpha(text, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(image, to);
        SetAlpha(text, to);
    }

    private void SetAlpha(Graphic graphic, float alpha)
    {
        if (graphic != null)
        {
            Color c = graphic.color;
            c.a = alpha;
            graphic.color = c;
        }
    }
}
