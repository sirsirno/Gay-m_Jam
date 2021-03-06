using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMe : MonoBehaviour
{
    [SerializeField] private Vector3 rotate;

    void Update()
    {
        transform.Rotate(rotate * Time.deltaTime);    
    }
}
