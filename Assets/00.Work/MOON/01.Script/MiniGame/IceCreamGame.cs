using System;
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
            //게임 승리
        }
        private bool Check(Vector3 trans)
        {
            float x  = trans.x;
            float y = trans.y;
            float left  = iceCreamRect.position.x - iceCreamRect.rect.width  / 2;
            float right = iceCreamRect.position.x + iceCreamRect.rect.width  / 2;
            float up = iceCreamRect.position.y + iceCreamRect.rect.height / 2;
            float down = iceCreamRect.position.y - iceCreamRect.rect.height / 2;
            if (x >= left && x <= right && y >= down && y <= up)
            {
                return true;
            }
            return false;
        }
    }
}
