using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    private PlayerInput playerInput = null;
    private PlayerEffect playerEffect = null;
    private SpriteRenderer sr = null;

    private Vector2 size = Vector2.zero;

    private Vector2 currentPos = Vector2.zero;
    private IInteractable currentObj = null;

    public Vector2 CurrentPos { get { return currentPos; } }

    [Header("��������")]
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float backSpeed = 5f;
    [SerializeField] private LayerMask whatIsInteract;

    [Space(10f)]
    [SerializeField] private Property myProperty = Property.FIRE;
    public Property MyProperty { get { return myProperty; } }


    private Vector2 target = Vector2.zero;
    public Vector2 Target { get { return target; } }


    private Vector2 targetLerp = Vector2.zero;
    public Vector2 TargetLerp { get { return targetLerp; } }


    private bool isMoving = false;
    public bool IsMoving { get { return isMoving; } }


    private void Start()
    {
        playerEffect = GetComponent<PlayerEffect>();
        playerInput = GetComponent<PlayerInput>();
        sr = GetComponent<SpriteRenderer>();

        size = transform.localScale;
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

                //if()

                if (interact.ChangeProperty(myProperty))
                {
                    playerEffect.PlayParticle(ParticleType.BOMB);
                    currentObj?.ChangeProperty(Property.NONE);

                    currentPos = obj.transform.position;
                    currentObj = interact;
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

            targetLerp = Vector2.Lerp(transform.position, target, moveSpeed * Time.deltaTime);
            transform.position = targetLerp;
        }

        if (!isMoving)
        {
            transform.position = Vector2.Lerp(transform.position, currentPos, backSpeed * Time.deltaTime);
        }
    }

    public void Init(Vector2 pos, IInteractable inter)
    {
        transform.position = pos;

        currentPos = transform.position;
        currentObj = inter;
    }
}
