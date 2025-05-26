using UnityEngine;

namespace _00.Work.KLM._01.Script.KAY_UI
{
    public class KAY_AutoUIScale : MonoBehaviour
    {
        void Start()
        {
            Camera camera = GetComponent<Camera>();
            Rect rect = camera.rect;
            float scaleheight = ((float)Screen.width / Screen.height) / ((float)16 / 9); // (°¡·Î / ¼¼·Î)
            float scalewidth = 1f / scaleheight;
            if (scaleheight < 1)
            {
                rect.height = scaleheight;
                rect.y = (1f - scaleheight) / 2f;
            }
            else
            {
                rect.width = scalewidth;
                rect.x = (1f - scalewidth) / 2f;
            }
            camera.rect = rect;
        }

        void OnPreCull() => GL.Clear(true, true, Color.black);
    }
}