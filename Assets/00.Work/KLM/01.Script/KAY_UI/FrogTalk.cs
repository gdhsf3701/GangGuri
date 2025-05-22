using System;
using TMPro;
using UnityEngine;

namespace _00.Work.KLM._01.Script.KAY_UI
{
    public class FrogTalk : MonoBehaviour
    {
      string[] _talks = { "그거 아시나요? 깽구리라는 \n이름은 갱(Gang)과 \n개구리의 합성어에요.", "도시를 구경해본 개구리로써, \n우물안이 더 좋은것 같아요 ㅎㅎ",
            "개구리와 거북이가 경주한다면,\n누가 이길까요? \n저는 재빠른 개구리라 당연히\n 이기겠죠?" , "오늘 저녁은 김치찌개 어때요?\n별로라면 된장찌개?" ,
            "저는 삼겹살보단 \n항정살이 좋아요!" , "저는 소도둑이\n 바늘도둑이 될거랍니다!"  , "저의 식사는 급식이 \n 아니랍니다~~ 부럽죠? " , 
            "전 카푸어일까요? \n 아니면.. 푸어카?","깽구리에 G가 몇개인지 \n 아시나요? ㅎㅎ" ,"제 인생의 절반을 \n 차 위에서 보냈어요 :>", "저는 우물학교 전교1등!","개발자가 일 안해요:<","저도 한 50년 \n 전에는 잘 나갔어요",
            "깽깽구리구리","코드 777을 입력하셔서,\n 지금 당장 VIP99를 달성하세요" ,"죄송합니다! 저희에게 다시\n 앗. 지금건 실수에요!"
      };
        [SerializeField] private TextMeshProUGUI talkText;

        private void Awake()
        {
            talkText.text = "저를 클릭하시면, 말동무가 \n되어드릴게요!";
        }

        public void ChangeTalk()
        {
            talkText.text = _talks[UnityEngine.Random.Range(0, _talks.Length)];
        }
    }
}
