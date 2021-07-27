using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObj : InteractableObj
{
    public string eventName;

    protected virtual void Start()
    {
        currentType = ObjType.TRIGGER;
    }

    protected override void OnChangeProperty(Property prop)
    {
        print("ÄÑÁ³À½");
    }
}
