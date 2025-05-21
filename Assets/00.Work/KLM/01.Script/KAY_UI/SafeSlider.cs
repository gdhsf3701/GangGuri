using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SafeSliderEnforcer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Slider slider;

    private bool pointerOver = false;

    void Update()
    {
        if (!pointerOver && slider.interactable)
        {
            // 포인터가 슬라이더를 벗어났는데 아직 켜져있으면 강제로 꺼버림
            slider.interactable = false;
        }

        // 마우스 눌렸을 때만 슬라이더 켜기
        if (pointerOver && UnityEngine.Input.GetMouseButtonDown(0))
        {
            slider.interactable = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pointerOver = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!pointerOver)
        {
            slider.interactable = false;
        }
    }
}