using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObj : InteractableObj
{
    public string eventName;

    private void Start()
    {
        currentType = ObjType.TRIGGER;
    }

    protected override void OnChangeProperty(Property prop)
    {
        print("ÄÑÁ³À½");
    }
}
