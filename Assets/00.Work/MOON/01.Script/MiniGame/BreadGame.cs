using System.Collections.Generic;
using _00.Work.JYE._01.Script.StealUI;
using _00.Work.MOON._01.Script.SO.MiniGame;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _00.Work.MOON._01.Script.MiniGame
{
    public class BreadGame : MonoBehaviour
    {
        [SerializeField] private GameObject breadCutPrefab;
        [SerializeField] private GameObject breadWantCutPrefab;
        [SerializeField] private MiniGameInputSO input;
        [SerializeField] private RectTransform parentObject;
        [SerializeField] private int cutMany;

        private List<RectTransform> _breadWantCutList = new List<RectTransform>();
        private int warningCount;
        [SerializeField]private int warningMaxCount;
        
        [SerializeField] private RectTransform start;
        [SerializeField] private RectTransform end;
        

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
            float startX = start.position.x;
            float endX = end.position.x;
            float segmentWidth = (endX - startX) / cutMany;
            float xNowPos = startX;

            for (int i = 1; i <= cutMany; i++)
            {
                float min = xNowPos;
                float max = startX + segmentWidth * i;
                Vector3 position = new Vector3(Random.Range(min, max), 720, 0);
                xNowPos = max;
                _breadWantCutList.Add(Instantiate(breadWantCutPrefab, position, Quaternion.identity, parentObject)
                    .GetComponent<RectTransform>());
            }
        }

        private void HandleMouseClick() 
        {
            Vector2 mousePosition = Mouse.current.position.value;
            RectTransform trans =
                Instantiate(breadCutPrefab, new Vector3(mousePosition.x, 720, 0), Quaternion.identity, parentObject)
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
                warningCount++;
                if (warningCount >= warningMaxCount)
                {
                    GameOver();
                }
            }
        }

        private void GameClear()
        {
            print("gameClear");
            input.OnMouseClick -= HandleMouseClick;
            ClockUI.OnSuccess?.Invoke();
        }

        private void GameOver()
        {
            print("gameOver");
            input.OnMouseClick -= HandleMouseClick;
            ClockUI.OnFail?.Invoke();
        }

        private bool Check(RectTransform trans)
        {
            float left1 = trans.position.x - trans.rect.width / 2;
            float right1 = trans.position.x + trans.rect.width / 2;

            for (int i = 0; i < _breadWantCutList.Count; i++)
            {
                RectTransform breadWantCut = _breadWantCutList[i];
                float left2 = breadWantCut.position.x - breadWantCut.rect.width / 2;
                float right2 = breadWantCut.position.x + breadWantCut.rect.width / 2;

                if (right1 >= left2 && left1 <= right2)
                {
                    _breadWantCutList.RemoveAt(i);
                    Destroy(breadWantCut.gameObject);
                    return true;
                }
            }
            return false;
        }
    }
}
