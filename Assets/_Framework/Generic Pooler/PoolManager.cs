using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Enemy Pooler", menuName = "Framework/Manager/Pool")]
public class PoolManager : ScriptableSingleton<PoolManager>
{
    // TODO: Pensar num jeito melhor para implementar isso
    public class PossiblePools
    {
        public Pool Bullets => PoolManager.Instance["Bullet"];
        public Pool BounceEnemies => PoolManager.Instance["BounceEnemy"];
    }

    [SerializeField]
    private List<Pool> poolsGroup;

    [HideInInspector]
    public PossiblePools possiblePools = new PossiblePools();

    public Pool this[string name]
    {
        get
        {
            var res = poolsGroup.Find(pool => pool.name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (!res)
            {
                Debug.LogError("O pooler que vc esta tentando acessar nao existe");
                Debug.Break();
            }

            return res;
        }
    }

    public void InitAllPools()
    {
        foreach (var pool in poolsGroup)
            pool.InitPool();
    }

    public void CloseAllPools()
    {
        foreach (var pool in poolsGroup)
            pool.ClosePool();
    }
}
