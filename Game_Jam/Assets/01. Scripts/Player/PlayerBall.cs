using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour, IDamage
{
    [SerializeField] private int damage = 1;

    [SerializeField] private float disableTime = 0.1f;
    private WaitForSeconds disableWait = null;

    private Collider2D coll = null;

    public int Damage 
    {
        get
        {
            return damage;
        }
    }

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        disableWait = new WaitForSeconds(disableTime);
    }

    private void OnEnable()
    {
        coll.enabled = true;
    }

    public void SetDisable(Enemy info)
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
