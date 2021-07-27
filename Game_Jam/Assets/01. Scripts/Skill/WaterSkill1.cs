using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSkill1 : Skill
{
    public float defaultDuration = 3f;
    private float duration = 3f;

    public int bulletCount = 5;

    public GameObject waterBullet;
    public GameObject bubbleEffect;

    public override void Using()
    {
        isActive = true;
        duration = defaultDuration;

        if (GameManager.Instance.enemyList.Count > 0)
        {
            for (int i = 0; i < bulletCount; i++)
            {
                Bullet_WaterSlow bullet = PoolManager.GetItem<Bullet_WaterSlow>();
                bullet.transform.position = GameManager.Instance.cameraHandler.waterTransform.position;

                int randomEnemy = Random.Range(0, GameManager.Instance.enemyList.Count);
                bullet.targetTransform = GameManager.Instance.enemyList[randomEnemy].transform;
            }
        }
    }

    private void Start()
    {
        GameManager.Instance.skills[2] = this;
        PoolManager.CreatePool<Bullet_WaterSlow>(waterBullet, transform, 5);
        PoolManager.CreatePool<Effect_Bubble>(bubbleEffect, transform, 5);
    }

    private void Update()
    {
        if (isActive)
        {
            duration -= Time.deltaTime;

            if (duration <= 0)
            {
                isActive = false;

            }
        }
    }
}
