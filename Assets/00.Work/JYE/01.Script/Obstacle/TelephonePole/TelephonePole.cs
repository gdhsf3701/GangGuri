using UnityEngine;
using _00.Work.MOON._01.Script.SO.Player;
using System.Collections;
using System;
using _00.Work.KLM.Sound;
using _00.Work.MOON._01.Script.Players;

public class TelephonePole : MonoBehaviour
{
    [Header("setting")]
    [SerializeField]private float time; //�� ������ �ð�
    [Space(10f)]
    //[Header("Need")]
    //[SerializeField] private PlayerInputSO inputSO; //��ǲ so
    private PlayerMovement move;

    private bool isFast; //true : ���� �� / false : ����
    

    private bool cantMove; //true : �� ������, false : Ǯ��
    [SerializeField] ParticleSystem particle;

    private void OnCollisionEnter(Collision collision)
    {
        if(!cantMove&&collision.gameObject.CompareTag("Player"))
        {
            move = collision.gameObject.GetComponentInChildren<PlayerMovement>();
            Rigidbody rd = collision.gameObject.GetComponent<Rigidbody>();
            rd.isKinematic = true;
                            
            isFast = !move.GetStatPerName("FAST");
            
            StopAllCoroutines();
            StartCoroutine(CantMove());
            rd.isKinematic = false;
        }
    }
    
    
    private IEnumerator CantMove() //�ð� ��ŭ �� ������.
    {
        
        particle.Play();    
        AudioSource audio = SoundManager.Instance.PlayWithLoop(SoundName.Thunder, gameObject);
        particle.Play();    
        cantMove = true;
        move.ChangeStatPer("STOP");
        yield return new WaitForSeconds(time);
        
        string s = isFast ? "FAST" : "NORMAL";
        move.ChangeStatPer(s);
        yield return new WaitForSeconds(time);
        cantMove = false;
        SoundManager.Instance.StopPlay(audio);
        particle.Stop();    
    }
}
