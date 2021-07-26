using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour, IDamage
{
    [SerializeField] private int damage = 1;

    [SerializeField] private float disableTime = 0.2f;
    private WaitForSeconds disableWait = null;

    private Collider2D coll = null;

    public int Damage 
    {
        get
        {
            return damage;
        }
    }

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        disableWait = new WaitForSeconds(disableTime);
    }

    public void SetDisable()
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        coll.enabled = false;
        yield return disableWait;
        coll.enabled = true;
    }
}
