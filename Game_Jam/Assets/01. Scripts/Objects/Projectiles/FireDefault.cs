using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireDefault : Projectile
{
    public float speed = 1;

    public GameObject defaultAttack;
    public GameObject QAttack;

    private void Start()
    {
        GameManager.Instance.QDefault = this;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, speed) * Time.deltaTime);
    }
}
