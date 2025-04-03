using UnityEngine;

[CreateAssetMenu(fileName = "obstacleSO", menuName = "SO/obstacleSO")]
public class ObstacleSO : ScriptableObject //Àå¾Ö¹°
{
    public eventType myEvent;
    public obstacleType myObstacle;


}

public enum eventType
{
    none,
    hailstone, //¿ì¹Ú
    fog, //¾È°³
}

public enum obstacleType
{
    none, //¾øÀ½
    cctv, //cctv
    bomb, //ÆøÅº
    ice, //¾óÀ½ ¶¥
    bumpy, //¿ïÅüºÒÅüÇÑ ¶¥
    sinkhole,//½ÌÅ©È¦
    manhole, //¸ÇÈ¦
    car, //ÀÚµ¿Â÷
    trash, //¾²·¹±â
    telephonePole, //Àüº¿´ë

}
