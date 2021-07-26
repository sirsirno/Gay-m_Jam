using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public ObjType objType { get; }

    public bool ChangeProperty(Property prop);
    public bool CheckPropertyLimit(Property prop);
}
