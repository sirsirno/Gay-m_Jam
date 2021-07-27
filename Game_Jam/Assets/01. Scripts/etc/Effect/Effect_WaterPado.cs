using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_WaterPado : Effect, IDamage
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float speed = 5;

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

    private new void Awake()
    {
        base.Awake();
        coll = GetComponent<Collider2D>();
        disableWait = new WaitForSeconds(disableTime);
    }

    private new void OnEnable()
    {
        base.OnEnable();
        coll.enabled = true;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void SetDisable(Enemy info)
    {
        StartCoroutine(Disable());
        info.SetSpeedEffect(0.5f, 5);
    }

    private IEnumerator Disable()
    {
        coll.enabled = false;
        yield return disableWait;
        coll.enabled = true;
    }
}