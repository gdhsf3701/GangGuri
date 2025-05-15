using System;
using System.Collections.Generic;
using _00.Work.MOON._01.Script.SO.MiniGame;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace _00.Work.MOON._01.Script.BreadCut
{
    public class BreadGame : MonoBehaviour
    {
        [SerializeField] private GameObject breadCutPrefab;
        [SerializeField] private GameObject breadWantCutPrefab;
        [SerializeField] private MiniGameInputSO input;
        [SerializeField] private RectTransform parentObject;
        [SerializeField] private int cutMany;

        private List<RectTransform> _breadWantCutList = new List<RectTransform>();
        [SerializeField]private int _warningCount;

        private void Awake()
        {
            input.OnMouseClick += HandleMouseClick;
            GameInit();
        }

        private void OnDestroy()
        {
            input.OnMouseClick -= HandleMouseClick;
        }

        private void GameInit()
        {
            float xPos = parentObject.rect.width / cutMany;
            float xNowPos = parentObject.rect.width - parentObject.rect.x;
            for (int i = 1; i <= cutMany; i++)
            {
                Vector3 position = new Vector3(Random.Range(xNowPos, xPos * i + parentObject.rect.x), 385, 0);
                xNowPos = xPos * i;
                _breadWantCutList.Add(Instantiate(breadWantCutPrefab, position, Quaternion.identity, parentObject)
                    .GetComponent<RectTransform>());
            }
        }

        private void HandleMouseClick()
        {
            Vector2 mousePosition = Mouse.current.position.value;
            RectTransform trans =
                Instantiate(breadCutPrefab, new Vector3(mousePosition.x, 385, 0), Quaternion.identity, parentObject)
                    .GetComponent<RectTransform>();
            if (_breadWantCutList.Count <= 0)
            {
                GameClear();
            }
            if (Check(trans))
            {
                if (_breadWantCutList.Count <= 0)
                {
                    GameClear();
                }
            }
            else
            {
                _warningCount++;
                if (_warningCount > cutMany / 2)
                {
                    GameOver();
                }
            }
        }

        private void GameClear()
        {
            print("gameClear");
            input.OnMouseClick -= HandleMouseClick;
            //성공 구현
        }

        private void GameOver()
        {
            print("gameOver");
            input.OnMouseClick -= HandleMouseClick;
            //실패 구현
        }

        private bool Check(RectTransform trans)
        {
            foreach (RectTransform breadWantCut in _breadWantCutList)
            {
                float left1  = trans.position.x - trans.rect.width  / 2;
                float right1 = trans.position.x + trans.rect.width  / 2;
                float left2  = breadWantCut.position.x - breadWantCut.rect.width  / 2;
                float right2 = breadWantCut.position.x + breadWantCut.rect.width  / 2;
                if (right1 >= left2 && left1  <= right2)
                {
                    _breadWantCutList.Remove(breadWantCut);
                    Destroy(breadWantCut.gameObject);
                    return true;
                }
            }
            return false;
        }
    }
}
