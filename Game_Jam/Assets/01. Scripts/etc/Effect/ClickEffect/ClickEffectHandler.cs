using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffectHandler : MonoBehaviour
{
    public GameObject effectObj = null;

    private Vector2 mousePos = Vector2.zero;

    void Start()
    {
        PoolManager.CreatePool<Effect_Ripple>(effectObj, transform, 5);
    }

    void Update()
    {
        CreateEffect();
    }

    private void CreateEffect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Effect_Ripple effect = PoolManager.GetItem<Effect_Ripple>();
            effect.transform.position = mousePos;
        }
    }
}
