using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMoveObj : InteractableObj
{
    protected bool isMove = false;

    [SerializeField] protected float speed = 3f;

    protected Rigidbody2D rb = null;

    protected virtual void Start()
    {
        currentType = ObjType.HUMAN;
    }

    protected virtual void SetMove(bool isMove)
    {
        this.isMove = isMove;
    }

    protected virtual void FixedUpdate()
    {
        if (!isMove) return;

        Move();
    }

    protected virtual void Move()
    {
        
    }

    protected override void OnChangeProperty(Property prop)
    {
        if (prop.Equals(Property.NONE))
        {
            SetMove(false);
        }
        else
        {
            SetMove(true);
        }
    }
}
