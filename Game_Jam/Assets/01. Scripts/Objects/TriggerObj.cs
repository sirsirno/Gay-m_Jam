using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObj : InteractableObj
{
    private void Start()
    {
        currentType = ObjType.TRIGGER;
    }

    protected override void OnChangeProperty(Property prop)
    {
        
    }
}
