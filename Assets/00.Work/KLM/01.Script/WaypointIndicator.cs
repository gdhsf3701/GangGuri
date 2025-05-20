using TMPro;
using UnityEngine;

namespace _00.Work.KLM._01.Script
{
    public class WaypointIndicator : MonoBehaviour
    {
        [SerializeField] private RectTransform arrowUI;      // 방향 화살표
        [SerializeField] private TextMeshProUGUI distanceText;
        [SerializeField] private Transform player;           // 플레이어
        [SerializeField] private Transform target;           // 목표 지점

        [SerializeField] private Camera cam;
        [SerializeField] private float borderPadding = 50f;

        private void Update()
        {
            if (target == null || player == null) return;

            Vector3 screenPos = cam.WorldToScreenPoint(target.position);
            bool isBehind = screenPos.z < 0;

            if (isBehind)
            {
                // 목표가 카메라 뒤에 있으면 반대로
                screenPos *= -1;
            }

            Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Vector2 screenPos2D = new Vector2(screenPos.x, screenPos.y);

            Vector2 dir = (screenPos2D - screenCenter).normalized;
            float maxX = Screen.width - borderPadding;
            float maxY = Screen.height - borderPadding;
            Vector2 cappedScreenPos = screenCenter + dir * 1000f;

            cappedScreenPos.x = Mathf.Clamp(cappedScreenPos.x, borderPadding, maxX);
            cappedScreenPos.y = Mathf.Clamp(cappedScreenPos.y, borderPadding, maxY);

            // 화면 안이면 목표 위치 근처에, 밖이면 테두리에
            bool isOffScreen = screenPos.z < 0 || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height;

            arrowUI.gameObject.SetActive(true); 
            arrowUI.position = isOffScreen ? cappedScreenPos : screenPos2D;
            arrowUI.up = dir;

            // 거리 텍스트
            float distance = Vector3.Distance(player.position, target.position);
            distanceText.text = $"{distance:F1}m";
            distanceText.transform.position = arrowUI.position + (Vector3)(dir * 155f);
        }

        // 동적으로 목표 설정하는 함수
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }
    }
}
