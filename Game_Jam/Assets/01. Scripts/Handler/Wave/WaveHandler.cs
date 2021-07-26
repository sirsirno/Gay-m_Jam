using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public List<WaveInfo> waveInfos = new List<WaveInfo>();
    private int waveIdx = 0;

    [SerializeField] private float waveTime = 20f;
    private WaitForSeconds waveWait = null;

    private void Start()
    {
        waveWait = new WaitForSeconds(waveTime);

        StartCoroutine(WaveLifeTime());
    }

    private IEnumerator WaveLifeTime()
    {
        yield return new WaitForSeconds(2f);

        while (waveIdx < waveInfos.Count)
        {
            StartWave(waveIdx);
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
        print($"{enemyType}을 {floor}에 소환");
    }
}
