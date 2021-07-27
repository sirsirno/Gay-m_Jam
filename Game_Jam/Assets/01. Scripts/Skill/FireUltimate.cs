using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUltimate : MonoBehaviour
{
    public int damage = 10;

    private void Start()
    {
        EventManager.AddEvent("FireBomb", FireBomb);
    }

    void FireBomb()
    {
        foreach(Enemy enemy in GameManager.Instance.enemyList)
        {
            enemy.GetDamage(damage);
        }
    }
}
