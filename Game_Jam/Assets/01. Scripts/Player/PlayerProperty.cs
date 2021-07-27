using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperty : MonoBehaviour
{
    private PlayerInput playerInput = null;
    private PlayerEffect playerEffect = null;
    private SpriteRenderer sr = null;

    private Vector2 size = Vector2.zero;

    private Vector2 lastPos = Vector2.zero;
    private Vector2 currentPos = Vector2.zero;

    private IInteractable lastObj = null;
    private IInteractable currentObj = null;

    public Vector2 CurrentPos { get { return currentPos; } }

    [Header("수정값들")]
    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float pathSpeed = 3f;
    [SerializeField] private float backSpeed = 5f;
    [SerializeField] private LayerMask whatIsInteract;
    [SerializeField] private LayerMask whatIsPath;

    [Space(10f)]
    [SerializeField] private Property myProperty = Property.FIRE;
    public Property MyProperty { get { return myProperty; } }


    private Vector2 target = Vector2.zero;
    public Vector2 Target { get { return target; } }


    private Vector2 targetLerp = Vector2.zero;
    public Vector2 TargetLerp { get { return targetLerp; } }


    private bool isMoving = false;
    public bool IsMoving { get { return isMoving; } }

    private bool isRiding = false;

    private Vector2 pathDir = Vector2.zero;
    private bool isGoingPath = false;

    [SerializeField] private float playerAlpha = 1f;

    private void Start()
    {
        playerEffect = GetComponent<PlayerEffect>();
        playerInput = GetComponent<PlayerInput>();
        sr = GetComponent<SpriteRenderer>();

        size = transform.localScale * 1.7f;
    }

    private void Update()
    {
        if (isGoingPath) return;

        GetMouse();
        MovePosition();
        CheckPath();
    }

    private void GetMouse()
    {
        if (playerInput.mouseDown)      // 마우스를 눌렀을 때
        {
            Vector2 dir = playerInput.mousePos - currentPos;

            if (dir.sqrMagnitude <= (size.x * size.x))     // 범위 안에 있다면
            {
                if (!isMoving)
                {
                    isMoving = true;
                    playerEffect.SetAlphaValue(1);
                }
            }
        }

        else if (playerInput.mouseUp)    // 마우스를 땟을 때
        {
            isMoving = false;

            Collider2D obj = Physics2D.OverlapPoint(transform.position, whatIsInteract);

            if (obj != null)
            {
                // 속성 변경
                IInteractable interact = obj.GetComponent<InteractableObj>();

                if (interact == null || interact.objType.Equals(ObjType.PATH) || interact.objType.Equals(ObjType.TRIGGER))          // 패스라면 리턴
                {
                    playerEffect.SetAlphaValue(playerAlpha);
                    return;
                }

                if (interact != currentObj && interact.ChangeProperty(myProperty))
                {
                    playerEffect.PlayParticle(ParticleType.BOMB);
                    playerEffect.SetAlphaValue(playerAlpha);
                    currentObj?.ChangeProperty(Property.NONE);

                    currentPos = obj.transform.position;
                    currentObj = interact;

                    if (currentObj.objType.Equals(ObjType.HUMAN))
                    {
                        transform.SetParent(obj.transform);
                        isRiding = true;
                    }
                    else
                    {
                        transform.SetParent(null);
                        isRiding = false;
                    }
                }
            }
            else
            {
                playerEffect.SetAlphaValue(playerAlpha);
            }
        }
    }

    private void MovePosition()
    {
        if (isRiding)
        {
            currentPos = transform.parent.position;
        }

        if (isMoving)                   // 누르고 있는 상태
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

    private void CheckPath()
    {
        if (isRiding) return;

        bool isInput = false;

        if (playerInput.W)
        {
            pathDir = Vector2.up;
            isInput = true;
        }
        else if (playerInput.A)
        {
            pathDir = Vector2.left;
            isInput = true;
        }
        else if (playerInput.S)
        {
            pathDir = Vector2.down;
            isInput = true;
        }
        else if (playerInput.D)
        {
            pathDir = Vector2.right;
            isInput = true;
        }

        if (isInput)
        {
            Collider2D obj = Physics2D.OverlapCircle(currentPos + pathDir, 0.05f, whatIsPath);

            if (obj != null)
            {
                InteractableObj interact = obj.transform.parent.parent.GetComponent<InteractableObj>();

                if (interact != null && interact.objType.Equals(ObjType.PATH) && interact.CheckPropertyLimit(myProperty))
                {
                    //print("가능합니다");

                    PathObj pathObj = interact.GetComponent<PathObj>();

                    PathData pathData = pathObj.GetPathEnd(obj.transform);

                    if (pathData == null)
                    {
                        return;
                    }

                    currentObj.ChangeProperty(Property.NONE);

                    lastObj = pathData.obj;
                    lastPos = pathData.obj.transform.position;

                    if (pathData.obj.objType.Equals(ObjType.TRIGGER))       // 맨 마지막이 트리거라면
                    {
                        StartCoroutine(GoingPath(pathObj.paths, pathData.isReverse, true));
                    }
                    else
                    {
                        StartCoroutine(GoingPath(pathObj.paths, pathData.isReverse, false));
                    }
                }
            }
        }
    }

    private IEnumerator GoingPath(List<Transform> paths, bool isReverse, bool isTrigger)
    {
        int idx = isReverse ? paths.Count - 1 : 0;

        Vector2 target = Vector2.zero;
        Vector2 myPos = Vector2.zero;

        myPos = transform.position;

        bool isPathEnd = false;

        playerEffect.SetAlphaValue(1);
        isGoingPath = true;

        while ((lastPos - myPos).sqrMagnitude > 0.01f)
        {
            if (!isPathEnd)
            {
                target = paths[idx].position;
            }
            else
            {
                target = lastPos;
            }

            transform.position = Vector2.MoveTowards(transform.position, target, pathSpeed * Time.deltaTime);

            if (!isPathEnd && transform.position.Equals(paths[idx].position))
            {
                idx += isReverse ? -1 : 1;
            }

            if (isReverse && idx.Equals(-1))
            {
                isPathEnd = true;
            }
            else if (!isReverse && idx.Equals(paths.Count))
            {
                isPathEnd = true;
            }

            myPos = transform.position;

            yield return null;
        }

        playerEffect.SetAlphaValue(playerAlpha);
        lastObj.ChangeProperty(myProperty);

        if (isTrigger)              // 트리거라면 다시 되돌아간다
        {
            lastPos = currentPos;
            lastObj = currentObj;

            isReverse = !isReverse;

            idx = isReverse ? paths.Count - 1 : 0;
            myPos = transform.position;
            isPathEnd = false;

            while ((lastPos - myPos).sqrMagnitude > 0.01f)
            {
                if (!isPathEnd)
                {
                    target = paths[idx].position;
                }
                else
                {
                    target = lastPos;
                }

                transform.position = Vector2.MoveTowards(transform.position, target, pathSpeed * Time.deltaTime);

                if (!isPathEnd && transform.position.Equals(paths[idx].position))
                {
                    idx += isReverse ? -1 : 1;
                }

                if (isReverse && idx.Equals(-1))
                {
                    isPathEnd = true;
                }
                else if (!isReverse && idx.Equals(paths.Count))
                {
                    isPathEnd = true;
                }

                myPos = transform.position;

                yield return null;
            }
        }

        currentObj = lastObj;
        currentPos = lastPos;

        isGoingPath = false;
    }

    public void Init(Vector2 pos, IInteractable inter)
    {
        transform.position = pos;

        currentPos = transform.position;
        currentObj = inter;
    }
}
