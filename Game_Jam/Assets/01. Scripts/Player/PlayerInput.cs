using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool mouseDown { get; private set; }
    public bool mouseUp { get; private set; }

    public Vector2 mousePos { get; private set; }

    private void Update()
    {
        mouseDown = Input.GetMouseButtonDown(0);
        mouseUp = Input.GetMouseButtonUp(0);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
