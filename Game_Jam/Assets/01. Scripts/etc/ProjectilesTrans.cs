using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesTrans : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.projectilesTrans = this.transform;
    }
}
