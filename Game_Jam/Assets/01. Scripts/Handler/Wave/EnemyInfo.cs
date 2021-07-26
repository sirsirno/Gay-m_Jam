using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfo
{
    [Header("�� Ÿ��")]
    public EnemyType enemyType;
    [Header("���� ���� �� ��")]
    public int floor;
    [Header("���� �����ǰ� ��ٸ� �ð�")]
    public float waitTime = 1f;
}
