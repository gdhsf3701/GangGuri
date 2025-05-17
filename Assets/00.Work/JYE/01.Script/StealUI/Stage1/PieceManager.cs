using UnityEngine;

namespace _00.Work.JYE._01.Script.StealUI.Stage1
{
    public class PieceManager : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] private int pieceCount = 8; //조각 개수
        [SerializeField] private Sprite[] pieceSprites; //이미지들
        [Space(10)]
        [Header("Need")]
        [SerializeField] private GameObject piecePrefab; //조각
        [SerializeField] GameObject drag; // 드래그 하는 중 부모
        [SerializeField] GameObject scroll; //스크롤 부모

        private void Awake()
        {
            Setting();
        }

        public Sprite[] GetSprites() //스프라이트 전달하기
        {
            return pieceSprites;
        }

        private void Setting() //카드 생성해주기
        {
            for (int i = 0; i < pieceCount; i++)
            {
                GameObject newPiece = Instantiate(piecePrefab, scroll.transform); //생성
                newPiece.transform.SetParent(scroll.transform);
                newPiece.SetActive(true);
                
                ImageDrag sc = newPiece.GetComponent<ImageDrag>(); //카드 세팅
                sc.ImageDragSetting(drag,scroll, pieceSprites[i]);
            }
        }
    }
}
