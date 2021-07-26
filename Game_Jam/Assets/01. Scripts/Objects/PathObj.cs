using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObj : InteractableObj
{
    private void Start()
    {
        currentType = ObjType.PATH;
    }

    protected override void OnChangeProperty(Property prop)
    {
        
    }
}
