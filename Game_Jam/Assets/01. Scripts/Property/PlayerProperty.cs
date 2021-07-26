using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    private PlayerInput playerInput = null;
    private SpriteRenderer sr = null;

    private Vector2 size = Vector2.zero;

    private Vector2 currentPos = Vector2.zero;

    [Header("수정값들")]
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float backSpeed = 5f;
    [SerializeField] private LayerMask whatIsInteract;

    [Space(10f)]
    [SerializeField] private Property myProperty = Property.FIRE;

    private Vector2 target = Vector2.zero;
    private bool isMoving = false;


    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        sr = GetComponent<SpriteRenderer>();

        size = transform.localScale;

        // currentPos를 시작지점으로
        currentPos = transform.position;
    }

    private void Update()
    {
        GetMouse();
        MovePosition();
    }

    private void GetMouse()
    {
        if (playerInput.mouseDown)      // 마우스를 눌렀을 때
        {
            Vector2 dir = playerInput.mousePos - currentPos;

            if (dir.sqrMagnitude <= (size.x * size.x))     // 범위 안에 있다면
            {
                isMoving = true;
            }
        }

        else if(playerInput.mouseUp)    // 마우스를 땟을 때
        {
            Collider2D obj = Physics2D.OverlapPoint(transform.position, whatIsInteract);

            if (obj != null)
            {
                // 속성 변경
                IInteractable interact = obj.GetComponent<InteractableObj>();

                if (interact.ChangeProperty(myProperty))
                {
                    currentPos = obj.transform.position;
                }
            }

            isMoving = false;
        }
    }

    private void MovePosition()
    {
        if (isMoving)                   // 누르고 있는 상태
        {
            target = playerInput.mousePos;

            Vector2 dir = target - currentPos;
            //print(dir.magnitude);

            if (dir.sqrMagnitude > (maxDistance * maxDistance))
            {
                target = currentPos + dir.normalized * maxDistance;
            }

            transform.position = Vector2.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
        }

        if (!isMoving)
        {
            transform.position = Vector2.Lerp(transform.position, currentPos, backSpeed * Time.deltaTime);
        }
    }
}
