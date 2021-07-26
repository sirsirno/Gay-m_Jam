using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    [Header("적 타입")]
    public EnemyType enemyType;
    [Header("적이 나올 층 수")]
    public int floor;
    [Header("적이 생성되고 기다릴 시간")]
    public float waitTime = 1f;
}
