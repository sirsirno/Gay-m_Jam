using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_SpearAttack : MonoBehaviour, IState
{
    private Rigidbody2D rb = null;
    private Vector2 attackDir = Vector2.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnEnter()
    {
        rb.velocity = Vector2.zero;

        if (attackDir.normalized.y < 0 || attackDir.normalized.x < 0)
        {
            return;
        }

        if (attackDir.normalized.y > 0.8f)
        {
            rb.AddForce(attackDir * 12f, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(attackDir * 10f, ForceMode2D.Impulse);
        }
    }

    public void OnEnd()
    {

    }

    public void SetValue(Vector2 attackDir)
    {
        this.attackDir = attackDir;
    }
}
