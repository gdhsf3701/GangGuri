using System.Collections;
using _00.Work.JYE._01.Script.Save;
using UnityEngine;

namespace _00.Work.JYE._01.Script.Manager
{
    //앞으로 삭제에 관해 땡겨주기를 작성.
    //로드 씬에만 존재 (그것도 로드 카드들의 부모)
    public class LoadCardManager : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private GameObject warning; //삭제 경고창(독촉)
        [SerializeField] private GameObject loadCardPrefabs; //카드
    
        private bool isEmpty; //true : 비었음, false : 꽉 찼음(비워줘야 함) (타이틀에서 정해준 그 bool 값)
        private void Start()
        {
            LoadCardSetting();
            warning.SetActive(false);

            LoadCard.OnCardDelete += LoadCardDelete;
        
            isEmpty = System.Convert.ToBoolean(PlayerPrefs.GetInt("SaveFileEmpty")); //bool 값으로 전환
            if (!isEmpty) //꽉 찼는지
            {
                StartCoroutine(Warning());
            }
        }

        private void LoadCardDelete(int deleteNum) //로드 카드 삭제 
        {
        }

        private void LoadCardSetting() //로드 카드들 불러와주기 (세팅)
        {
            for (int i = 0; i < SaveManager.AllSaveNum; i++)
            {
                GameObject card = Instantiate(loadCardPrefabs, gameObject.transform);
                card.SetActive(true);
            }
        }

        private IEnumerator Warning() //경고문 띄우기
        {
            warning.SetActive(true);
            yield return new WaitForSeconds(3.1f);
            warning.SetActive(false);
        }
    }
}
