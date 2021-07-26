using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMoveObj : InteractableObj
{
    private bool isMove = false;

    private void Start()
    {
        currentType = ObjType.HUMAN;
    }

    private void SetMove(bool isMove)
    {
        this.isMove = isMove;

        if (isMove)
        {
            print("øÚ¡˜¿Œ¥Á");
        }
        else
        {
            print("∏ÿ√·¥Á");
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
