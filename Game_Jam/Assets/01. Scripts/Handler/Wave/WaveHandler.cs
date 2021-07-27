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
    [SerializeField] private List<Transform> floors = new List<Transform>();

    private void Start()
    {
        floors = floorTrans.GetComponentsInChildren<Transform>().ToList();
        floors.RemoveAt(0);

        waveWait = new WaitForSeconds(waveTime);

        EventManager.AddEvent("OnGameStart", StartWave);
    }

    private void StartWave()
    {
        StartCoroutine(WaveLifeTime());
    }

    private IEnumerator WaveLifeTime()
    {
        yield return new WaitForSeconds(2f);

        while (waveIdx < waveInfos.Count)
        {
            waveIdx++;
            GameManager.Instance.uiManager.SetWaveNumber(waveIdx);
            GameManager.Instance.uiManager.SetLeftNumber(GameManager.Instance.uiManager.currentLeft);
            GameManager.Instance.uiManager.TitleWave();
            // 웨이브 시작 애니메이션

            yield return new WaitForSeconds(1f);

            yield return StartCoroutine(StartWave(waveIdx - 1));

            yield return waveWait;
        }
    }

    private IEnumerator StartWave(int waveIdx)
    {
        int count = GameManager.Instance.uiManager.currentLeft;

        for(int i = 0; i < waveInfos[waveIdx].enemyInfos.Count;i++)
        {
            count += waveInfos[waveIdx].enemyInfos[i].createCount;
        }
        GameManager.Instance.uiManager.SetLeftNumber(count);

        foreach (var enemy in waveInfos[waveIdx].enemyInfos)
        {
            for (int i = 0; i < enemy.createCount; i++)
            {
                CreateEnemy(enemy.enemyType, enemy.floor);
                yield return new WaitForSeconds(enemy.waitTime);
            }
        }

        yield break;
    }

    private void CreateEnemy(EnemyType enemyType, int floor)
    {
        // print($"{enemyType}을 {floor}에 소환");

        Enemy enemy = null;

        switch (enemyType)
        {
            case EnemyType.NORMAL:

                enemy = PoolManager.GetItem<Enemy_Normal>();
                break;

            case EnemyType.SPEED:

                enemy = PoolManager.GetItem<Enemy_Speed>();
                break;

            case EnemyType.TANK:

                enemy = PoolManager.GetItem<Enemy_Tank>();
                break;

            case EnemyType.BOSS:

                enemy = PoolManager.GetItem<Enemy_Normal>();
                break;
        }

        GameManager.Instance.enemyList.Add(enemy);
        enemy.GetComponent<Move_GoRight>().SetValue(enemy.defaultSpeed);

        for (int i = 0; i < enemy.transform.childCount; i++)
        {
            enemy.transform.GetChild(i).gameObject.SetActive(false);
        }
        enemy.transform.position = GetFloor(floor - 1);
    }

    private Vector2 GetFloor(int idx)
    {
        if (idx > (floors.Count - 1)) return Vector2.zero;

        return floors[idx].position;
    }
}
