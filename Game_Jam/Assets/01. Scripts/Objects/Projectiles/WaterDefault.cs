using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDefault : Projectile
{
    public GameObject bulletObj = null;
    private GameObject targetObj;

    void Awake()
    {
        PoolManager.CreatePool<Bullet_Water>(bulletObj, GameManager.Instance.projectilesTrans, 5);
    }

    public override void Create()
    {
        Bullet_Water bullet = PoolManager.GetItem<Bullet_Water>();
        bullet.transform.position = transform.position;

        for (int i = 0; i < GameManager.Instance.enemyList.Count; i++)
        {
            if (targetObj != null)
            {
                float originDis = Vector2.Distance(transform.position, targetObj.transform.position);
                float newDis = Vector2.Distance(transform.position, GameManager.Instance.enemyList[i].transform.position);
                if (originDis > newDis)
                {
                    targetObj = GameManager.Instance.enemyList[i].gameObject;
                }
            }
            else targetObj = GameManager.Instance.enemyList[i].gameObject;
        }

        if(GameManager.Instance.enemyList.Count == 0)
        {
            targetObj = null;
        }

        if (targetObj != null)
        {
            bullet.targetTransform = targetObj.transform;
        }
        else
        {
            bullet.gameObject.SetActive(false);
        }
    }
}
