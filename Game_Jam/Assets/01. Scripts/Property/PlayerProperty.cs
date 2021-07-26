using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    private PlayerInput playerInput = null;
    private SpriteRenderer sr = null;

    private Vector2 size = Vector2.zero;

    private Vector2 currentPos = Vector2.zero;

    [Header("��������")]
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

        // currentPos�� ������������
        currentPos = transform.position;
    }

    private void Update()
    {
        GetMouse();
        MovePosition();
    }

    private void GetMouse()
    {
        if (playerInput.mouseDown)      // ���콺�� ������ ��
        {
            Vector2 dir = playerInput.mousePos - currentPos;

            if (dir.sqrMagnitude <= (size.x * size.x))     // ���� �ȿ� �ִٸ�
            {
                isMoving = true;
            }
        }

        else if(playerInput.mouseUp)    // ���콺�� ���� ��
        {
            Collider2D obj = Physics2D.OverlapPoint(transform.position, whatIsInteract);

            if (obj != null)
            {
                // �Ӽ� ����
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
        if (isMoving)                   // ������ �ִ� ����
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
