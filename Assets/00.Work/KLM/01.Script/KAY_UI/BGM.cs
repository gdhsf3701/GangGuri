using System;
using _00.Work.KLM._01.Script.KAY_UI;
using _00.Work.KLM.Sound;
using UnityEngine;

public class BGM : MonoBehaviour
{

    [SerializeField] private SoundName soundName;

    private void Start()
    {
        Debug.Log($"Trying to play BGM: {soundName}");
        SoundManager.Instance.PlayBGM(soundName);
    }
}
