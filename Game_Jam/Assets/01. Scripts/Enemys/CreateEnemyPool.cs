using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyPool : MonoBehaviour
{
    public GameObject enemy_Test = null;

    private void Awake()
    {
        PoolManager.CreatePool<Enemy_Test>(enemy_Test, transform, 10);
    }
}
