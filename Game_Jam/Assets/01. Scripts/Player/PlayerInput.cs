using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool W { get; private set; }
    public bool A { get; private set; }
    public bool S { get; private set; }
    public bool D { get; private set; }

    public bool mouseDown { get; private set; }
    public bool mouseUp { get; private set; }

    public Vector2 mousePos { get; private set; }

    private void Update()
    {
        mouseDown = Input.GetMouseButtonDown(0);
        mouseUp = Input.GetMouseButtonUp(0);
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        W = Input.GetKeyDown(KeyCode.W);
        A = Input.GetKeyDown(KeyCode.A);
        S = Input.GetKeyDown(KeyCode.S);
        D = Input.GetKeyDown(KeyCode.D);
    }
}
