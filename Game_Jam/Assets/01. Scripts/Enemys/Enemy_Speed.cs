using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Speed : Enemy
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1f;

    private Animator anim = null;
    private readonly int onReady = Animator.StringToHash("OnReady");
    private readonly int isAttack = Animator.StringToHash("IsAttack");

    private Move_GoRight move = null;
    private Attack_SpearAttack attack = null;

    private Transform playerTrans = null;

    private bool can_attack = true;
    private WaitForSeconds attackWait = null;

    protected void Awake()
    {
        IState create = gameObject.AddComponent<State_Empty>();
        dicState.Add(State.Create, create);

        attack = gameObject.AddComponent<Attack_SpearAttack>();
        dicState.Add(State.Attack, attack);

        move = gameObject.AddComponent<Move_GoRight>();
        move.SetValue(speed);
        dicState.Add(State.Move, move);

        IState die = gameObject.AddComponent<State_Empty>();
        dicState.Add(State.Die, die);

        IState defau = gameObject.AddComponent<State_Empty>();
        dicState.Add(State.Default, defau);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        attackWait = new WaitForSeconds(attackCooldown);

        StartCoroutine(CoolDown());
    }

    protected override IEnumerator LifeTime()
    {
        yield return null;

        while (true)
        {
            playerTrans = CheckDistance();

            if (playerTrans != null && can_attack)
            {
                SetState(State.Default);

                // ¼±µô
                anim.SetTrigger(onReady);
                yield return new WaitForSeconds(0.7f);

                anim.SetBool(isAttack, true);
                SpearAttack();

                can_attack = false;

                // ÈÄµô
                yield return new WaitForSeconds(1f);
                anim.SetBool(isAttack, false);
            }

            SetState(State.Move);

            yield return null;
        }
    }

    private IEnumerator CoolDown()
    {
        while (true)
        {
            if (!can_attack)
            {
                yield return attackWait;
                can_attack = true;
            }

            yield return null;
        }
    }

    private Transform CheckDistance()
    {
        foreach (var player in GameManager.Instance.playerList)
        {
            if ((player.transform.position - transform.position).sqrMagnitude < (attackRange * attackRange))
            {
                if ((player.transform.position - transform.position).normalized.y > 0 && (player.transform.position - transform.position).normalized.x > 0)      // ÇÏ´ÜÃ¢, µÞÃ¢ ¸·±â¿ë
                {
                    return player.transform;
                }
            }
        }

        return null;
    }

    private void SpearAttack()
    {
        attack.SetValue((playerTrans.transform.position - transform.position).normalized);
        SetState(State.Attack);
    }

    public override void SetDisable()
    {
        base.SetDisable();
        can_attack = true;
    }
}
