using System;
using _00.Work.JYE._01.Script.StealUI;
using _00.Work.KLM.Sound;
using _00.Work.MOON._01.Script.SO.MiniGame;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace _00.Work.MOON._01.Script.MiniGame
{
    public class IceCreamGame : MonoBehaviour
    {
        [SerializeField] private Sprite[] iceCreamSprite;
        [SerializeField] private Image iceCream;
        [SerializeField] private RectTransform iceCreamRect;
        [SerializeField] private int iceCreamSpiteChangeCount;
        [SerializeField] private MiniGameInputSO input;
        [SerializeField] private int gameWinCount;
        
        private int _clickCount = 0;
        private int _iceCreamCount = 0;

        private void Awake()
        {
            iceCream.sprite = iceCreamSprite[0];
            SoundManager.Instance.PlayBGM(SoundName.Stage4BGM);
            input.OnMouseClick += HandleMouseClick;
        }

        private void OnDestroy()
        {
            input.OnMouseClick -= HandleMouseClick;
        }

        private void HandleMouseClick()
        {
            Vector2 mousePosition = Mouse.current.position.value;
            if (Check(mousePosition))
            {
                if (++_clickCount%iceCreamSpiteChangeCount == 0)
                {
                    iceCream.sprite = iceCreamSprite[++_iceCreamCount];
                    if (_clickCount >= gameWinCount)
                    {
                        GameWin();
                    }
                }
            }
        }

        public void GameOver()
        {
            Debug.Log("Game Over");
            input.OnMouseClick -= HandleMouseClick;
        }

        private void GameWin()
        {
            Debug.Log("Game Win");
            input.OnMouseClick -= HandleMouseClick;
            ClockUI.OnSuccess?.Invoke();
        }
        private bool Check(Vector3 screenPosition)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                iceCreamRect, screenPosition, null);
        }
    }
}
