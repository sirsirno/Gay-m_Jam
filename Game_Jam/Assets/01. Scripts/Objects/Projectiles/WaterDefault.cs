using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDefault : Projectile
{
    public GameObject bulletObj = null;

    void Awake()
    {
        PoolManager.CreatePool<Bullet_Water>(bulletObj, transform, 5);
    }

    public override void Create()
    {
        Bullet_Water bullet = PoolManager.GetItem<Bullet_Water>();


    }
}
