using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMoveObj : InteractableObj
{
    private bool isMove = false;

    [SerializeField] private float speed = 3f;

    private void Start()
    {
        currentType = ObjType.HUMAN;
    }

    private void SetMove(bool isMove)
    {
        this.isMove = isMove;

        /*
        if (isMove)
        {
            print("øÚ¡˜¿Œ¥Á");
        }
        else
        {
            print("∏ÿ√·¥Á");
        }
        */
    }

    private void Update()
    {
        if (!isMove) return;

        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
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
