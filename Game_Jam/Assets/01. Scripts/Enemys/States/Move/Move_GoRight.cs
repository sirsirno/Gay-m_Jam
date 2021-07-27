using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_GoRight : MonoBehaviour, IState
{
    private float moveSpeed = 3f;
    private Rigidbody2D rb = null;

    private Coroutine move = null;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnEnter()
    {
        move = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        yield return null;

        while (true)
        {
            //rb.MovePosition(rb.position + Vector2.right * moveSpeed * Time.fixedDeltaTime);
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            yield return null;
        }
    }

    public void OnEnd()
    {
        StopCoroutine(move);
        rb.velocity = Vector2.zero;
    }

    public void SetValue(float speed)
    {
        moveSpeed = speed;
    }

    public float GetValue()
    {
        return moveSpeed;
    }
}
