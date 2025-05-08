using _00.Work.MOON._01.Script.Core.DI;
using UnityEngine;

public class EnemyManager : MonoBehaviour , IDependencyProvider
{
    [SerializeField]
    private bool _isFinded = false;
    public bool IsFinded => _isFinded;
    
    [Provide]
    public EnemyManager ProvideEnemyManager() => this;

    public void Finded()
    {
        _isFinded = true;
    }
}
