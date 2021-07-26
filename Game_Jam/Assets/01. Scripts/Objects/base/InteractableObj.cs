using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObj : MonoBehaviour, IInteractable
{
    protected Property currentProperty = Property.NONE;
    protected ObjType currentType = ObjType.MOVE;

    [SerializeField] protected Property propertyLimit = Property.NONE;

    public bool ChangeProperty(Property prop)
    {
        if (!propertyLimit.HasFlag(prop))     // �ش� �Ӽ��� �ƴϸ�
        {
            return false;
        }

        currentProperty = prop;

        OnChangeProperty(prop);

        return true;
    }

    protected abstract void OnChangeProperty(Property prop);
}
