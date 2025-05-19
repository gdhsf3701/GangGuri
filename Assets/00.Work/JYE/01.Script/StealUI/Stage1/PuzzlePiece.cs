using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.JYE._01.Script.StealUI.Stage1
{
    public class PuzzlePiece : MonoBehaviour
    {
        private static List<bool> checkList = new List<bool>(); //맞는지 확인
        private static ClcokUI clock; //성공 유무

        private void Start()
        {
            checkList.Add(false);
        }

        public void Setting(Sprite s,ClcokUI c) //세팅하기
        {
            if (clock == null)
            {
                clock = c;
            }
            Image myImage = gameObject.GetComponent<Image>();
            myImage.sprite = s;
        }

        public static void Check(GameObject p,Sprite ch) //올바른 이미지가 들어갔는지 확인
        {
            Sprite mySprite = p.GetComponent<Image>().sprite;
            if (mySprite == ch) //이미지 맞는지 확인
            {
                checkList[p.transform.GetSiblingIndex()] = true;
                bool all = true;
                foreach (bool i in checkList) //전부 올바르게 들어갔는지 확인
                {
                    if (!i)
                    {
                        all = false;
                        return;
                    }
                }

                if (all) //성공
                {
                    checkList.Clear(); // 다음 게임때 원활하게 하기 위해서
                    clock.Success();
                }
            }
        }

        public void SetParent()
        {
            ImageDrag.SetParent(gameObject);
        }

        public void CancelSetParent()
        {
            ImageDrag.SetParent(null);
        }
    }
}
