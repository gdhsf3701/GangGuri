using _00.Work.KLM.Sound;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Enemies
{
    public class Police : Enemy
    {
        protected override void Awake()
        {
            base.Awake();
            SoundManager.Instance.PlayWithLoop(SoundName.Police, gameObject);
        }
    }
}