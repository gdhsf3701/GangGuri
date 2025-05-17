using UnityEngine;
using System;

namespace _00.Work.JYE._01.Script.StealUI.Stage1
{
    public class ImageDrag : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private GameObject drag; // 드래그 하는 중 부모
        [SerializeField] private GameObject scroll; //스크롤 부모
        [SerializeField] private GameObject puzzle; // 드롭 될 부모

        private GameObject curPiece; //현재 들고 있는 조각

        public void ClickPieceBtn(GameObject piece) //그림(조각)을 클릭함(하는중) (드래그)
        {
            curPiece =  piece;
            piece.transform.SetParent(drag.transform);
            //curPiece.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
