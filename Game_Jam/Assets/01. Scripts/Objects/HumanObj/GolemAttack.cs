using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonoBehaviour, IDamage
{
    private Collider2D coll = null;

    public int damage = 10;
    public int Damage 
    {
        get 
        {
            return damage;
        } 
    }

    private void Start()
    {
        coll = GetComponent<Collider2D>();

        SetCollider(false);
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetCollider(bool value)
    {
        coll.enabled = value;
    }

    public void SetDisable(Enemy hitInfo)
    {
        
    }
}
