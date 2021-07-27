using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    protected bool isActive = false;

    public bool IsActive { get { return isActive; } }

    public virtual void Using()
    {

    }
}
