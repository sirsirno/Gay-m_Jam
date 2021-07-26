using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireDefault : Projectile
{
    private void OnEnable()
    {
        transform.DORotate(new Vector3(0, 0, 360), 1).SetRelative(true).SetLoops(-1).SetEase(Ease.Linear);
    }
}
