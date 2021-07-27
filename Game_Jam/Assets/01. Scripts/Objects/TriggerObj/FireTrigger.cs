using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrigger : TriggerObj
{
    private bool isTriggered = false;

    protected override void OnChangeProperty(Property prop)
    {
        if (isTriggered) return;

        isTriggered = true;

        EventManager.Invoke(eventName);
    }
}
