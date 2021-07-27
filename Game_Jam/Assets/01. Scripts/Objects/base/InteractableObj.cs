using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObj : MonoBehaviour, IInteractable
{
    protected Property currentProperty = Property.NONE;
    public Property CurrentProperty
    {
        get
        {
            return currentProperty;
        }
    }

    protected ObjType currentType = ObjType.MOVE;

    [SerializeField] protected Property propertyLimit = Property.NONE;
    public Property PropertyLimit
    {
        get
        {
            return propertyLimit;
        }
    }

    public ObjType objType 
    {
        get
        {
            return currentType;
        }
    }

    public bool ChangeProperty(Property prop)
    {
        if (!propertyLimit.HasFlag(prop))     // 해당 속성이 아니면
        {
            return false;
        }

        currentProperty = prop;

        OnChangeProperty(prop);

        return true;
    }

    public bool CheckPropertyLimit(Property prop)
    {
        if (!propertyLimit.HasFlag(prop))     // 해당 속성이 아니면
        {
            return false;
        }

        return true;
    }

    protected abstract void OnChangeProperty(Property prop);
}
