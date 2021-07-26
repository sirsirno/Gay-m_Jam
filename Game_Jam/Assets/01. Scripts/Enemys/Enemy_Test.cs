using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Test : Enemy
{
    [SerializeField] private float speed = 3f;

    private Move_GoLeft move = null;

    protected void Awake()
    {
        IState create = gameObject.AddComponent<State_Empty>();
        dicState.Add(State.Create, create);

        IState attack = gameObject.AddComponent<State_Empty>();
        dicState.Add(State.Attack, attack);

        move = gameObject.AddComponent<Move_GoLeft>();
        move.SetValue(speed);
        dicState.Add(State.Move, move);

        IState die = gameObject.AddComponent<State_Empty>();
        dicState.Add(State.Die, die);
    }

    protected override IEnumerator LifeTime()
    {
        while (true)
        {
            SetState(State.Move);

            yield return null;
        }
    }
}
