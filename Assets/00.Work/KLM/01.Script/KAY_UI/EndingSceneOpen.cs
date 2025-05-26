using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _00.Work.KLM._01.Script.KAY_UI
{
    public class EndingSceneOpen : MonoBehaviour
    {
        private void Awake()
        {
            StartCoroutine(waitt());
        }

        IEnumerator waitt()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(25);
        }
    }
}