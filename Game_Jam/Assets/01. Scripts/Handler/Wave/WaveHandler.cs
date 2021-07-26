using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaveHandler : MonoBehaviour
{
    public List<WaveInfo> waveInfos = new List<WaveInfo>();
    private int waveIdx = 0;

    [SerializeField] private float waveTime = 20f;
    private WaitForSeconds waveWait = null;

    public Transform floorTrans = null;
    private List<Transform> floors = new List<Transform>();

    private void Start()
    {
        floors = floorTrans.GetComponentsInChildren<Transform>().ToList();
        floors.RemoveAt(0);

        waveWait = new WaitForSeconds(waveTime);

        StartCoroutine(WaveLifeTime());
    }

    private IEnumerator WaveLifeTime()
    {
        yield return new WaitForSeconds(2f);

        while (waveIdx < waveInfos.Count)
        {
            StartCoroutine(StartWave(waveIdx));
            waveIdx++;

            yield return waveWait;
        }
    }

    private IEnumerator StartWave(int waveIdx)
    {
        foreach (var enemy in waveInfos[waveIdx].enemyInfos)
        {
            CreateEnemy(enemy.enemyType, enemy.floor);
            yield return new WaitForSeconds(enemy.waitTime);
        }

        yield return null;
    }

    private void CreateEnemy(EnemyType enemyType, int floor)
    {
        // print($"{enemyType}을 {floor}에 소환");

        Enemy enemy = null;

        switch (enemyType)
        {
            case EnemyType.NORMAL:

                enemy = PoolManager.GetItem<Enemy_Test>();
                break;

            case EnemyType.SPEED:

                enemy = PoolManager.GetItem<Enemy_Test>();
                break;

            case EnemyType.TANK:

                enemy = PoolManager.GetItem<Enemy_Test>();
                break;

            case EnemyType.BOSS:

                enemy = PoolManager.GetItem<Enemy_Test>();
                break;
        }

        enemy.transform.position = GetFloor(floor - 1);
    }

    private Vector2 GetFloor(int idx)
    {
        if (idx >= (floors.Count - 1)) return Vector2.zero;

        return floors[idx].position;
    }
}
