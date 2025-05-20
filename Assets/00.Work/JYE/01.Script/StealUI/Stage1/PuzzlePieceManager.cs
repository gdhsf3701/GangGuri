using UnityEngine;

namespace _00.Work.JYE._01.Script.StealUI.Stage1
{
    public class PuzzlePieceManager : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] private int puzzleCount = 4; //맞는조각 개수
        [Space(10)]
        [Header("Need")]
        [SerializeField] private GameObject piecePiecePrefab; //맞는 카드
        [SerializeField] private PieceManager piece; //이미지 얻을려고
        [SerializeField] private ClockUI clock; //성공 유무
        [SerializeField]private Sprite[] sprites; //이미지들

        private void Awake()
        {
            Setting();
        }

        private void Setting() //카드 생성해주기
        {
            for (int i = 0; i < puzzleCount; i++)
            {
                GameObject newPiece = Instantiate(piecePiecePrefab, transform); //생성
                newPiece.transform.SetParent(transform);
                newPiece.SetActive(true);
                
                PuzzlePiece sc = newPiece.GetComponent<PuzzlePiece>(); //카드 세팅
                sc.Setting(sprites[i], clock);
            }
        }
    }
}
