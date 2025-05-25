using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.KLM._01.Script.KAY_UI
{
    [RequireComponent(typeof(CanvasScaler))]
    public class AutoUIScale : MonoBehaviour
    {
        private void Start()
        {
            CanvasScaler scaler = GetComponent<CanvasScaler>();

            // UI 스케일 모드가 ScreenSize 기반인지 확인
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;

            // 화면 해상도
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            float screenAspect = screenWidth / screenHeight;

            // 기준 비율 설정 (16:9 = 약 1.777)
            float targetAspect = 16f / 9f;

            // 화면이 target보다 더 세로로 긴 경우 → matchWidthOrHeight = 0 (가로 기준)
            // 화면이 target보다 더 가로로 넓은 경우 → matchWidthOrHeight = 1 (세로 기준)
            scaler.matchWidthOrHeight = (screenAspect < targetAspect) ? 0f : 1f;

            // 기준 해상도도 현재 해상도에 맞게 자동 세팅 (선택 사항)
            scaler.referenceResolution = new Vector2(screenWidth, screenHeight);

            Debug.Log($"[AutoUIScale] 화면비: {screenAspect:F2}, 기준: {(screenAspect < targetAspect ? "Width" : "Height")}, 해상도: {screenWidth}x{screenHeight}");
        }
    }
}