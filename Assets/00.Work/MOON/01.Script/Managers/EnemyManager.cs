using _00.Work.MOON._01.Script.Core.DI;
using _00.Work.MOON._01.Script.Enemies;
using _00.Work.MOON._01.Script.SO.Finder;
using UnityEngine;

public class EnemyManager : MonoBehaviour , IDependencyProvider
{
    [SerializeField]
    private bool _isFinded = false;
    public bool IsFinded => _isFinded;
    [SerializeField] private SpawnEnemySO spawnenemy;
    
    [Provide]
    public EnemyManager ProvideEnemyManager() => this;

    public void Finded()
    {
        _isFinded = true;
        spawnenemy.Target.Spawn();
    }
}
