using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected enum State { 
        Default,    // 아무것도 없는 상태
        Create,     // 생성될 때
        Move,       // 움직일 때
        Attack,     // 공격할 때
        Die         // 죽을 때
    }

    public float maxHp = 3f;
    protected float currHp = 0f;
    [System.NonSerialized] public float defaultSpeed = 3f;

    public bool is_Die = false;

    private SpriteRenderer myRend;
    protected SpriteRenderer MyRend 
    {
        get
        {
            if (myRend == null)
            {
                myRend = GetComponent<SpriteRenderer>();
            }

            return myRend;
        }
    }

    private readonly Color color_Trans = new Color(1f, 1f, 1f, 0.3f);
    private readonly WaitForSeconds colorWait = new WaitForSeconds(0.1f);

    protected State currentState = State.Create;
    protected Dictionary<State, IState> dicState = new Dictionary<State, IState>();

    protected Coroutine lifeTime = null;

    protected void OnEnable()
    {
        currHp = maxHp;
        MyRend.color = Color.white;
        is_Die = false;

        SetDefaultState(State.Create);
        lifeTime = StartCoroutine(LifeTime());
    }

    protected virtual void SetDefaultState(State state)     // 초기 행동 설정
    {
        currentState = state;
        dicState[currentState].OnEnter();
    }

    protected virtual void SetState(State state)
    {
        if (currentState.Equals(state)) return;

        dicState[currentState].OnEnd();
        currentState = state;
        dicState[currentState].OnEnter();
    }

    protected virtual void PlayState(State state)
    {
        dicState[state].OnEnter();
    }

    protected virtual IEnumerator LifeTime()
    {
        // 여기에 적의 로직 구현
        yield return null;
    }

    public virtual void GetDamage(int damage)
    {
        if (currentState.Equals(State.Die)) return;

        currHp -= damage;

        DamageIndicator.DamageDisplay(damage, this.gameObject);
        StoneFragEffectHandler.CreateStoneFrag(transform.position);
        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_StoneHit, 0.6f);
        //GameManager.Instance.cameraHandler.CameraImpulse(0.1f);

        StartCoroutine(Blinking());
        CheckHp();
    }

    protected virtual void CheckHp()
    {
        if (currentState.Equals(State.Die)) return;

        if (currHp <= 0f)
        {
            SetState(State.Die);
            StopCoroutine(lifeTime);
            is_Die = true;
            SetDisable();
        }
    }

    protected IEnumerator Blinking()
    {
        MyRend.color = Color.red;
        yield return colorWait;
        MyRend.color = Color.white;
    }

    public void SetHp(float hp) 
    {
        currHp = hp;
    }

    protected void OnTriggerEnter2D(Collider2D coll)
    {
        IDamage damage = coll.GetComponent<IDamage>();

        if (damage != null)
        {
            GetDamage(damage.Damage);
            damage.SetDisable(this);
        }
    }

    public virtual void SetDisable()
    {
        GameManager.Instance.enemyList.Remove(this);

        GameManager.Instance.cameraHandler.CameraImpulse(1f);
        StoneFragDeadEffectHandler.CreateStoneFrag(transform.position);
        SoundManager.Instance.PlaySFXSound(SoundManager.Instance.Audio_SFX_StoneBreak, 1f);

        GameManager.Instance.uiManager.SetLeftNumber(GameManager.Instance.uiManager.currentLeft - 1);
        GameManager.Instance.chainCount++;
        GameManager.Instance.uiManager.RefreshChainUI();
        GetComponent<Move_GoRight>().SetValue(defaultSpeed);

        print("사라진다");
        gameObject.SetActive(false);
    }

    public void SetSpeedEffect(float speed, float duration)
    {
        GetComponent<Move_GoRight>().SetValue(speed);

        if (gameObject.activeSelf)
        {
            StartCoroutine(SpeedEffect(duration));
        }
    }

    IEnumerator SpeedEffect(float duration)
    {
        yield return new WaitForSeconds(duration);
        GetComponent<Move_GoRight>().SetValue(defaultSpeed);
    }
}
