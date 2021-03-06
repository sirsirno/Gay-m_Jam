using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemObj : HumanMoveObj
{
    private enum GolemState
    {
        NONE,
        RIDING,
        MOVE,
        JUMP,
        GROUND_ATK,
        ZUKBANG_ATK
    }

    private Transform body = null;
    private Vector2 dir = Vector2.zero;

    private SpriteRenderer sr = null;
    private Animator anim = null;

    private GolemState currentState = GolemState.NONE;
    private readonly int IsFire = Animator.StringToHash("IsFire");
    private readonly int IsMove = Animator.StringToHash("IsMove");
    private readonly int IsJump = Animator.StringToHash("IsJump");
    private readonly int IsRiding = Animator.StringToHash("IsRiding");
    private readonly int OnGroundAtk = Animator.StringToHash("OnGroundAtk");
    private readonly int OnZukBangAtk = Animator.StringToHash("OnZukBangAtk");


    private WaitForSeconds ridingWait = new WaitForSeconds(0.5f);
    private WaitForSeconds zukbangAtkWait = new WaitForSeconds(2f);
    private WaitForSeconds groundAtkWait = new WaitForSeconds(1.5f);

    private WaitForSeconds pOneSecWait = new WaitForSeconds(0.1f);
    private WaitForSeconds pFiveSecWait = new WaitForSeconds(0.5f);

    private bool isGround = false;
    private bool checkGround = false;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform groundCheck = null;

    [SerializeField] private GolemAttack attack = null;

    [SerializeField] private GameObject inImg = null;

    protected override void Start()
    {
        base.Start();
        body = transform.parent;

        sr = body.GetComponent<SpriteRenderer>();
        rb = body.GetComponent<Rigidbody2D>();
        anim = body.GetComponent<Animator>();

        StartCoroutine(LifeTime());
    }

    protected override void FixedUpdate()
    {
        if (!isMove) return;
    }

    private IEnumerator LifeTime()
    {
        while (true)
        {
            yield return null;

            if (!isMove || currentState.Equals(GolemState.NONE))
            {
                 continue;
            }

            ChangeState();
            CheckGround();

            switch (currentState)
            {
                case GolemState.NONE:

                    break;

                case GolemState.RIDING:

                    Riding();
                    yield return ridingWait;

                    break;

                case GolemState.MOVE:

                    Move();

                    break;

                case GolemState.JUMP:

                    anim.SetBool(IsJump, true);
                    Move();

                    break;

                case GolemState.GROUND_ATK:

                    anim.SetBool(IsMove, false);
                    GroundAtk();
                    yield return groundAtkWait;

                    break;

                case GolemState.ZUKBANG_ATK:

                    anim.SetBool(IsMove, false);
                    ZukbangAtk();
                    yield return zukbangAtkWait;

                    break;
            }


            //yield return null;
        }
    }

    private void ChangeState()
    {
        if (Input.GetKey(KeyCode.S))                                // ????
        {
            if (currentState.Equals(GolemState.MOVE))
            {
                currentState = GolemState.GROUND_ATK;
            }
        }


        if (Input.GetKey(KeyCode.W))                                // ????
        {
            if (currentState.Equals(GolemState.MOVE))
            {
                currentState = GolemState.ZUKBANG_ATK;
            }
        }


        if (isGround && Input.GetKeyDown(KeyCode.Space))                            // ????
        {
            if (currentState.Equals(GolemState.MOVE))
            {
                currentState = GolemState.JUMP;
                Jump();
            }
        }
    }

    protected override void Move()
    {
        dir.x = 0;
        dir.y = rb.velocity.y;

        if (Input.GetKey(KeyCode.A))
        {
            sr.flipX = false;
            dir.x = -1 * speed;

            anim.SetBool(IsMove, true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            sr.flipX = true;
            dir.x = 1 * speed;

            anim.SetBool(IsMove, true);
        }
        else
        {
            anim.SetBool(IsMove, false);
        }

        rb.velocity = dir;
    }

    private void Riding()
    {
        anim.SetBool(IsRiding, true);
        currentState = GolemState.MOVE;
    }

    private void CheckGround()
    {
        Collider2D ground = Physics2D.OverlapCircle(groundCheck.position, 0.02f, whatIsGround);

        //print(ground == null);

        if (ground != null)
        {
            isGround = true;

            if (checkGround && currentState.Equals(GolemState.JUMP))
            {
                GameManager.Instance.cameraHandler.CameraImpulse(0.5f);
                anim.SetBool(IsJump, false);
                currentState = GolemState.MOVE;
            }
            //anim.SetBool(IsJump, false);
        }
        else
        {
            isGround = false;
            //anim.SetBool(IsJump, true);
        }
    }

    private void Jump()
    {
        //print("?????? ????");
        checkGround = false;

        //print("is")
        anim.SetBool(IsJump, true);
        rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);

        StartCoroutine(ChangeCheck());
    }

    private IEnumerator ChangeCheck()
    {
        yield return pOneSecWait;

        checkGround = true;
    }

    private void GroundAtk()
    {
        anim.SetTrigger(OnGroundAtk);
        currentState = GolemState.MOVE;
        //print("?????? ????");

        StartCoroutine(GroundAttack());
    }

    private IEnumerator GroundAttack()
    {
        attack.SetDamage(40);

        yield return pOneSecWait;
        yield return pOneSecWait;
        yield return pOneSecWait;

        attack.SetCollider(true);
        GameManager.Instance.cameraHandler.CameraImpulse(2f);
        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Golem_HitGround, 1);

        yield return new WaitForFixedUpdate();
        attack.SetCollider(false);
    }

    private void ZukbangAtk()
    {
        anim.SetTrigger(OnZukBangAtk);
        currentState = GolemState.MOVE;
        //print("?????? ????");

        StartCoroutine(ZukBangAttack());
    }

    private IEnumerator ZukBangAttack()
    {
        attack.SetDamage(10);

        attack.SetCollider(true);
        yield return new WaitForFixedUpdate();
        attack.SetCollider(false);

        GameManager.Instance.cameraHandler.CameraImpulse(1.5f);
        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Golem_Punch1, 0.3f);

        yield return pFiveSecWait;

        attack.SetDamage(10);

        attack.SetCollider(true);
        yield return new WaitForFixedUpdate();
        attack.SetCollider(false);

        GameManager.Instance.cameraHandler.CameraImpulse(1.5f);
        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Golem_Punch2, 0.3f);

        yield return pOneSecWait;
        yield return pOneSecWait;
        yield return pOneSecWait;
        yield return pOneSecWait;

        attack.SetDamage(50);

        attack.SetCollider(true);
        yield return new WaitForFixedUpdate();
        attack.SetCollider(false);

        GameManager.Instance.cameraHandler.CameraImpulse(2f);
        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_Golem_Punch2, 0.3f);
        yield return pFiveSecWait;
    }

    protected override void SetMove(bool isMove)
    {
        base.SetMove(isMove);

        if (isMove)
        {
            currentState = GolemState.RIDING;
        }
        else
        {
            anim.SetBool(IsRiding, false);
            currentState = GolemState.NONE;
        }
    }

    protected override void OnChangeProperty(Property prop)
    {
        base.OnChangeProperty(prop);

        if (prop.Equals(Property.FIRE))
        {
            anim.SetBool(IsFire, true);
            inImg.SetActive(false);
        }
        else if (prop.Equals(Property.WATER))
        {
            anim.SetBool(IsFire, false);
            inImg.SetActive(false);
        }
        else if (prop.Equals(Property.NONE))
        {
            inImg.SetActive(true);
        }
    }
}
