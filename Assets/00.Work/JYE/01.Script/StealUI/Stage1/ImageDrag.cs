using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace _00.Work.JYE._01.Script.StealUI.Stage1
{
    public class ImageDrag : MonoBehaviour
    {
        private GameObject drag; // 드래그 하는 중 부모
        private GameObject scroll; //스크롤 부모
        private static GameObject puzzle; // 드롭 될 부모
        
        private Sprite mySprite; //자신의 이미지
        private RectTransform rectTransform; //자신
        private Image myImage;

        private static ImageDrag currentPiece; // true : 눌린게 있다 / flase : 눌린게 없다
        private void Update()
        {
            if(currentPiece == this)
            {
                Vector2 mousePosition = Mouse.current.position.value;
                rectTransform.position = mousePosition;
            }
        }

        public void ImageDragSetting(GameObject d, GameObject s, Sprite image) //세팅하기
        {
            myImage = GetComponent<Image>();
            drag = d;
            scroll = s;
            myImage.sprite = image;
            mySprite = image;
            rectTransform =  GetComponent<RectTransform>();
        }

        public static void SetParent(GameObject p) //(드롭될)부모 정하기
        {
            if (currentPiece != null)
            {
                puzzle = p;
            }
        }
        public void ClickPiece() //그림(조각)을 클릭함
        {
            currentPiece =  this;
            transform.SetParent(drag.transform);
            myImage.raycastTarget = false;
        }

        public void CancelPiece() //클릭을 그만둠
        {
            if (currentPiece == this)
            {
                if (puzzle != null) //선택 된 게 있다면
                {
                    GetOut(puzzle.transform); // 선객 보내버리기
                    transform.SetParent(puzzle.transform);
                    rectTransform.position = puzzle.transform.position;
                    PuzzlePiece.Check(puzzle, mySprite);
                    SetParent(null); //오류 없애기
                }
                else //다시 돌아가라
                {
                    transform.SetParent(scroll.transform);
                }
                myImage.raycastTarget = true;
                currentPiece = null;
            }
        }

        private void GetOut(Transform t) //간 곳에 선객(다른 조각)이 있다면
        {
            if (t.childCount > 0)
            {
                foreach (Transform c in t)
                {
                    c.transform.SetParent(scroll.transform);
                }
            }
        }
        
    }
}
