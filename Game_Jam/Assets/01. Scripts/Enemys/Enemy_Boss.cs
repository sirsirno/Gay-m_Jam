using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy
{
    private Move_GoRight move = null;

    protected void Awake()
    {
        defaultSpeed = 2f;

        IState create = gameObject.AddComponent<State_Empty>();
        dicState.Add(State.Create, create);

        IState attack = gameObject.AddComponent<State_Empty>();
        dicState.Add(State.Attack, attack);

        move = gameObject.AddComponent<Move_GoRight>();
        move.SetValue(defaultSpeed);
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
