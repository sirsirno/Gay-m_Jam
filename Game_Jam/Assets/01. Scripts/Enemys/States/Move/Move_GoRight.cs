using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_GoRight : MonoBehaviour, IState
{
    private float moveSpeed = 3f;

    private Coroutine move = null;

    private void Start()
    {

    }

    public void OnEnter()
    {
        move = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (true)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            yield return null;
        }
    }

    public void OnEnd()
    {
        StopCoroutine(move);
    }

    public void SetValue(float speed)
    {
        moveSpeed = speed;
    }
}
