using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Water : Effect, IDamage
{
    [SerializeField] private int damage = 3;

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public void SetDisable()
    {
        
    }
}
