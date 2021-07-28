using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemyPool : MonoBehaviour
{
    public GameObject enemy_Normal = null;
    public GameObject enemy_Speed = null;
    public GameObject enemy_Tank = null;
    public GameObject enemy_Boss = null;

    private void Awake()
    {
        PoolManager.CreatePool<Enemy_Normal>(enemy_Normal, transform, 10);
        PoolManager.CreatePool<Enemy_Speed>(enemy_Speed, transform, 10);
        PoolManager.CreatePool<Enemy_Tank>(enemy_Tank, transform, 10);
        PoolManager.CreatePool<Enemy_Boss>(enemy_Boss, transform, 2);
    }
}
